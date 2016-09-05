using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ExpressionHandler
{
   
 
    internal  class CollectPropertyFromExpression  : ExpressionVisitor
    {
        private List<PropertyInfo> sb = new List<PropertyInfo>();

        internal List<PropertyInfo>  Translate(Expression fieldUpdate)
        {
            this.Visit(fieldUpdate);
            return sb;
        }


        protected override Expression VisitMember(MemberExpression node)
        {
            EntityInfo entity = new EntityInfo(node.Member.ReflectedType);
            PropertyInfo p = entity.GetPropertyByPropertyName(node.Member.Name);
            sb.Add(p);
            return base.VisitMember(node);
        }





    }

}