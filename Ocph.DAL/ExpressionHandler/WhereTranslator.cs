using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Ocph.DAL.ExpressionHandler
{
    internal class WhereTranslator : ExpressionVisitor
    {

        private StringBuilder sb;
        private string _orderBy = string.Empty;
        private int? _skip = null;
        private int? _take = null;
        private string _whereClause = string.Empty;



        public string Translate(Expression expression)
        {
            this.sb = new StringBuilder();
            sb.Append(" ");
            this.Visit(expression);
            _whereClause = this.sb.ToString();
            return _whereClause;
        }

        protected override Expression VisitLambda<T>(Expression<T> node)
        {

            return base.VisitLambda<T>(node);
        }



        protected override Expression VisitMethodCall(MethodCallExpression m)
        {

            if (m.NodeType == ExpressionType.Call && m.Method.Name == "Contains")
            {
               var c= this.Visit(m.Object);
                sb.Append(" Like ");
                foreach (var x in m.Arguments)
                {
                    if (x.NodeType == ExpressionType.Constant)
                    {
                        sb.Append("'%");
                        this.Visit(x);
                        sb.Append("%'");
                    }

                    if (x.NodeType == ExpressionType.MemberAccess)
                    {
                        sb.Append("'%");
                        this.Visit(x);
                        sb.Append("%'");
                    }
                }
                return m;
            }


            if (m.NodeType == ExpressionType.Call && m.Method.Name == "Equals")
            {
                if (m.Object != null)
                {
                    this.Visit(m.Object);
                    sb.Append(" = ");
                }

                foreach (var x in m.Arguments)
                {
                    var a = this.Visit(x);

                }
                return m;
            }


            if (m.Method.DeclaringType == typeof(Queryable) && m.Method.Name == "Quet")
            {

            }

            if (m.Method.Name == "Substring")
            {
                sb.Append("substring(");

                this.Visit(m.Object);
                sb.Append(",");

                if(m.Arguments.Count>1)
                {
                        Visit( m.Arguments[0]);
                        sb.Append(",");
                        Visit(m.Arguments[1]);
                    
                }else
                {
                    Visit(m.Arguments);
                }

                

                sb.Append(")");
            }

            if (m.Method.Name == "Any")
            {
                sb.Append(" Exists ");
                foreach (var arg in m.Arguments)
                {
                    Visit(m.Arguments);
                }
            }


            if (m.Method.DeclaringType == typeof(Queryable) && m.Method.Name == "Select")
            {
                sb.Append("(Select ");
                if (m.Object != null)
                {
                    this.Visit(m.Object);
                    sb.Append(" = ");
                }

                var a = (Expression)m.Arguments.Where(O => O.NodeType == ExpressionType.Quote).FirstOrDefault();
                if (a != null)
                {
                    Visit(a);
                    //a.Type.DeclaringType


                    sb.Append(" From ");
                }

                var call = (Expression)m.Arguments.Where(O => O.NodeType == ExpressionType.Call).FirstOrDefault();
                if (call != null)
                {

                    Visit(call);
                }

                return m;
            }

            if (m.Method.Name == "Where")
            {

                Expression e = (Expression)m.Arguments[0];
                var ty = e.Type.GenericTypeArguments[0].GenericTypeArguments[0];
                if (ty != null)
                {
                    EntityInfo entity = new EntityInfo(ty);
                    sb.Append(entity.TableName).Append(" ");
                }

                sb.Append(" Where ");
                foreach (var x in m.Arguments)
                {

                    this.Visit(x);
                }

                sb.Append(")");
                return m;
            }
            else if (m.Method.Name == "Take")
            {
                if (this.ParseTakeExpression(m))
                {
                    Expression nextExpression = m.Arguments[0];
                    return this.Visit(nextExpression);
                }
            }
            else if (m.Method.Name == "Skip")
            {
                if (this.ParseSkipExpression(m))
                {
                    Expression nextExpression = m.Arguments[0];
                    return this.Visit(nextExpression);
                }
            }
            else if (m.Method.Name == "OrderBy")
            {
                if (this.ParseOrderByExpression(m, "ASC"))
                {
                    Expression nextExpression = m.Arguments[0];
                    return this.Visit(nextExpression);
                }
            }
            else if (m.Method.Name == "OrderByDescending")
            {
                if (this.ParseOrderByExpression(m, "DESC"))
                {
                    Expression nextExpression = m.Arguments[0];
                    return this.Visit(nextExpression);
                }
            }

            return m;
        }


        protected override Expression VisitUnary(UnaryExpression u)
        {
            switch (u.NodeType)
            {
                case ExpressionType.Not:
                    sb.Append(" NOT ");
                    this.Visit(u.Operand);
                    break;
                case ExpressionType.Convert:
                    this.Visit(u.Operand);
                    break;

                case ExpressionType.Quote:
                    this.Visit(u.Operand);
                    break;
                default:
                    throw new NotSupportedException(string.Format("The unary operator '{0}' is not supported", u.NodeType));
            }
            return u;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        protected override Expression VisitBinary(BinaryExpression b)
        {

            this.Visit(b.Left);

            switch (b.NodeType)
            {
                case ExpressionType.And:
                    sb.Append(" AND ");
                    break;

                case ExpressionType.AndAlso:
                    sb.Append(" AND ");
                    break;

                case ExpressionType.Or:
                    sb.Append(" OR ");
                    break;

                case ExpressionType.OrElse:
                    sb.Append(" OR ");
                    break;

                case ExpressionType.Equal:
                    if (IsNullConstant(b.Right))
                    {
                        sb.Append(" IS ");
                    }
                    else
                    {
                        sb.Append(" = ");
                    }
                    break;

                case ExpressionType.NotEqual:
                    if (IsNullConstant(b.Right))
                    {
                        sb.Append(" IS NOT ");
                    }
                    else
                    {
                        sb.Append(" <> ");
                    }
                    break;

                case ExpressionType.LessThan:
                    sb.Append(" < ");
                    break;

                case ExpressionType.LessThanOrEqual:
                    sb.Append(" <= ");
                    break;

                case ExpressionType.GreaterThan:
                    sb.Append(" > ");
                    break;

                case ExpressionType.GreaterThanOrEqual:
                    sb.Append(" >= ");
                    break;

                default:
                    throw new NotSupportedException(string.Format("The binary operator '{0}' is not supported", b.NodeType));

            }

            this.Visit(b.Right);
            sb.Append(" ");
            return b;
        }


        protected override Expression VisitConstant(ConstantExpression node)
        {
            if (node.NodeType == ExpressionType.Constant)
            {
                switch (node.Type.Name)
                {
                    case "String":
                        sb.Append("'" + node.Value + "'");
                        break;
                    case "Int32":
                        sb.Append(node.Value);
                        break;
                    case "Boolean":
                        sb.Append("'" + node.Value.ToString() + "'");
                        break;
                    default:
                        if(node.Type.IsEnum)
                        {
                            sb.Append("'" + node.Value.ToString() + "'");
                        }else
                          Visit(node);
                        break;
                }
            }
            return node;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            // sb.Append(" "+node.Name+" ");
            return base.VisitParameter(node);
        }
        protected override Expression VisitMember(MemberExpression m)
        {

            if (m.Expression != null)
            {
                if (m.NodeType == ExpressionType.MemberAccess && m.Expression.NodeType == ExpressionType.Parameter)
                {
                    Visit(m.Expression);
                }

                if (m.NodeType == ExpressionType.MemberAccess && 
                    m.Expression.NodeType == ExpressionType.Parameter && 
                    m.Member.MemberType == MemberTypes.Property)
                {
                    EntityInfo entity = new EntityInfo(m.Member.ReflectedType);
                    PropertyInfo p = entity.GetPropertyByPropertyName(m.Member.Name);
              var a=      p.GetType();
                    sb.Append(entity.TableName).Append(".").Append(entity.GetAttributDbColumn(p));
                }


                if (m.NodeType == ExpressionType.MemberAccess &&
                    m.Expression.NodeType == ExpressionType.MemberAccess &&
                    m.Member.MemberType == MemberTypes.Property)
                {
                    if (m.Member.ReflectedType.IsValueType == false)
                    {
                        var val = Expression.Lambda(m).Compile().DynamicInvoke();
                        sb.Append(Helpers.ConverConstant(val));
                    }
                    else
                    {
                        if (m.Member.ReflectedType.Name == "DateTime")
                        {
                            MemberExpression e = (MemberExpression)m.Expression;

                            if (e.Expression.NodeType == ExpressionType.Parameter)
                            {
                                sb.Append(m.Member.Name + "(");
                                Visit(m.Expression);
                                sb.Append(") ");
                            }


                            if (e.Expression.NodeType == ExpressionType.Constant)
                            {
                                var val = Expression.Lambda(m).Compile().DynamicInvoke();
                                sb.Append(Helpers.ConverConstant(val));
                            }


                        }

                    }


                    //  Visit(m.Expression);
                }


                

                if (m.NodeType == ExpressionType.MemberAccess && 
                    m.Expression.NodeType == ExpressionType.Constant &&
                    m.Member.MemberType == MemberTypes.Field)
                {
                    var val = Expression.Lambda(m).Compile().DynamicInvoke();
                    sb.Append(Helpers.ConverConstant(val));
                }



                if (m.NodeType == ExpressionType.MemberAccess &&
                    m.Expression.NodeType == ExpressionType.MemberAccess &&
                    m.Member.MemberType == MemberTypes.Field)
                {
                    var val = Expression.Lambda(m).Compile().DynamicInvoke();
                    sb.Append(Helpers.ConverConstant(val));
                }

                if (m.NodeType == ExpressionType.MemberAccess &&
                    m.Expression.NodeType == ExpressionType.Constant &&
                   m.Member.MemberType == MemberTypes.Property)
                {
                    var val = Expression.Lambda(m).Compile().DynamicInvoke();
                    sb.Append(Helpers.ConverConstant(val));
                }

            }
            else
            {
                if (m.Member.ReflectedType.IsValueType == true && m.Member.MemberType == MemberTypes.Property)
                {
                    if(m.Member.GetType().IsEnum)
                    {
                        var val = Expression.Lambda(m).Compile().DynamicInvoke();
                        sb.Append(Helpers.ConverConstant(val));
                    }
                    else
                    {
                        var val = Expression.Lambda(m).Compile().DynamicInvoke();
                        sb.Append(Helpers.ConverConstant(val));
                    }

                }
            }

            return m.Expression;


        }

        protected bool IsNullConstant(Expression exp)
        {
            if (exp.NodeType == ExpressionType.Constant)
            {
                var a = (ConstantExpression)exp;
                if (a.Value != null)
                    return false;
                else
                    return true;

            }
            else

                return (exp.NodeType == ExpressionType.Constant && ((ConstantExpression)exp).Value == null);
        }

        private bool ParseOrderByExpression(MethodCallExpression expression, string order)
        {
            UnaryExpression unary = (UnaryExpression)expression.Arguments[1];
            LambdaExpression lambdaExpression = (LambdaExpression)unary.Operand;
            // lambdaExpression = Evaluator.PartialEval(lambdaExpression) as LambdaExpression;
            MemberExpression body = lambdaExpression.Body as MemberExpression;
            if (body != null)
            {
                if (string.IsNullOrEmpty(_orderBy))
                {
                    _orderBy = string.Format("{0} {1}", body.Member.Name, order);
                }
                else
                {
                    _orderBy = string.Format("{0}, {1} {2}", _orderBy, body.Member.Name, order);
                }

                return true;
            }

            return false;
        }

        private bool ParseTakeExpression(MethodCallExpression expression)
        {
            ConstantExpression sizeExpression = (ConstantExpression)expression.Arguments[1];

            int size;
            if (int.TryParse(sizeExpression.Value.ToString(), out size))
            {
                _take = size;
                return true;
            }

            return false;
        }

        private bool ParseSkipExpression(MethodCallExpression expression)
        {
            ConstantExpression sizeExpression = (ConstantExpression)expression.Arguments[1];

            int size;
            if (int.TryParse(sizeExpression.Value.ToString(), out size))
            {
                _skip = size;
                return true;
            }

            return false;
        }
    }
}
