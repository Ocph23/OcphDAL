using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ExpressionHandler
{
    internal class SelectTranslator : ExpressionVisitor
    {
        private StringBuilder sb = new StringBuilder();
        private string _updateQuery;
        private bool come;

        public string UpdateQuery
        {
            get { return _updateQuery; }
            set { _updateQuery = value; }
        }


        internal string Translate<T>(Expression<Func<T, dynamic>> func)
        {
            this.sb = new StringBuilder();
            this.Visit(func);
            _updateQuery = sb.ToString();
            return _updateQuery;
        }

        protected override Expression VisitMethodCall(MethodCallExpression m)
        {

            if (m.NodeType == ExpressionType.Call)
            {
                foreach (var a in m.Arguments)
                    this.Visit(a);
            }


            if (m.Method.DeclaringType == typeof(Queryable) && m.Method.Name == "Select")
            {
                foreach (var x in m.Arguments)
                {
                    if (x.NodeType == ExpressionType.Constant)
                    {
                        var a = this.Visit(x);

                    }
                    else
                    {
                        this.Visit(x);
                    }
                }
            }

            if (m.Method.DeclaringType == typeof(Queryable) && m.Method.Name == "Where")
            {
                foreach (var x in m.Arguments)
                {
                    this.Visit(x);
                }
                return m;
            }
            else if (m.Method.Name == "Take")
            {
                throw new NotImplementedException();
            }
            else if (m.Method.Name == "Skip")
            {
                throw new NotImplementedException();
            }
            else if (m.Method.Name == "OrderBy")
            {
                throw new NotImplementedException();
            }
            else if (m.Method.Name == "OrderByDescending")
            {
                throw new NotImplementedException();
            }

            return m;
        }


        protected override Expression VisitBinary(BinaryExpression node)
        {
            return base.VisitBinary(node);
        }

        protected override Expression VisitConditional(ConditionalExpression node)
        {
            return base.VisitConditional(node);
        }

        protected override Expression VisitDynamic(DynamicExpression node)
        {
            return base.VisitDynamic(node);
        }

        protected override Expression VisitExtension(Expression node)
        {
            return base.VisitExtension(node);
        }


        protected override Expression VisitConstant(ConstantExpression node)
        {
            return base.VisitConstant(node);
        }


        protected override Expression VisitMember(MemberExpression node)
        {
            if (come == true)
                sb.Append(", ");
            MemberExpression m = (MemberExpression)node;
            sb.Append(m.Expression.ToString()).Append(".").Append(m.Member.Name);

            come = true;
            return base.VisitMember(node);
        }


    }
}
