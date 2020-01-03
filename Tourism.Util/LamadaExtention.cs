using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Tourism.Eums;

namespace Tourism.Util
{
    public class LamadaExtention<Dto> where Dto : new()
    {
        private List<Expression> m_lstExpression = null;
        private ParameterExpression m_Parameter = null;

        public LamadaExtention()
        {
            m_lstExpression = new List<Expression>();
            m_Parameter = Expression.Parameter(typeof(Dto), "x");
        }

        //构造表达式，存放到m_lstExpression集合里面
        public void GetExpression(string strPropertyName, object strValue, ExpressionTypeEnum expressType)
        {
            Expression expRes = null;
            MemberExpression member = Expression.PropertyOrField(m_Parameter, strPropertyName);
            if (expressType == ExpressionTypeEnum.Contains)
            {
                expRes = Expression.Call(member, typeof(string).GetMethod("Contains"), Expression.Constant(strValue));
            }
            else if (expressType == ExpressionTypeEnum.Equal)
            {
                expRes = Expression.Equal(member, Expression.Constant(strValue, member.Type));
            }
            else if (expressType == ExpressionTypeEnum.LessThan)
            {
                expRes = Expression.LessThan(member, Expression.Constant(strValue, member.Type));
            }
            else if (expressType == ExpressionTypeEnum.LessThanOrEqual)
            {
                expRes = Expression.LessThanOrEqual(member, Expression.Constant(strValue, member.Type));
            }
            else if (expressType == ExpressionTypeEnum.GreaterThan)
            {
                expRes = Expression.GreaterThan(member, Expression.Constant(strValue, member.Type));
            }
            else if (expressType == ExpressionTypeEnum.GreaterThanOrEqual)
            {
                expRes = Expression.GreaterThanOrEqual(member, Expression.Constant(strValue, member.Type));
            }
            //return expRes;
            m_lstExpression.Add(expRes);
        }

        //针对Or条件的表达式
        public void GetExpression(string strPropertyName, List<object> lstValue)
        {
            Expression expRes = null;
            MemberExpression member = Expression.PropertyOrField(m_Parameter, strPropertyName);
            foreach (var oValue in lstValue)
            {
                if (expRes == null)
                {
                    expRes = Expression.Equal(member, Expression.Constant(oValue, member.Type));
                }
                else
                {
                    expRes = Expression.Or(expRes, Expression.Equal(member, Expression.Constant(oValue, member.Type)));
                }
            }


            m_lstExpression.Add(expRes);
        }

        //多个字段or同一个值
        public void GetExpression(List<string> listStrPropertyName, object strValue, ExpressionTypeEnum expressType)
        {
            Expression expRes = null;

            foreach (var itemValue in listStrPropertyName)
            {
                MemberExpression member = Expression.PropertyOrField(m_Parameter, itemValue);
                if (expressType == ExpressionTypeEnum.Contains)
                {
                    if (expRes == null)
                    {
                        expRes = Expression.Call(member, typeof(string).GetMethod("Contains"), Expression.Constant(strValue));
                        //expRes = Expression.Equal(member, Expression.Constant(strValue, member.Type));
                    }
                    else
                    {
                        expRes = Expression.Or(expRes, Expression.Call(member, typeof(string).GetMethod("Contains"), Expression.Constant(strValue)));
                    }
                }
                else
                {
                    if (expRes == null)
                    {
                        expRes = Expression.Equal(member, Expression.Constant(strValue, member.Type));
                    }
                    else
                    {
                        expRes = Expression.Or(expRes, Expression.Equal(member, Expression.Constant(strValue, member.Type)));
                    }
                }
            }
            m_lstExpression.Add(expRes);
        }

        //得到Lamada表达式的Expression对象
        public Expression<Func<Dto, bool>> GetLambda()
        {
            Expression whereExpr = null;
            foreach (var expr in m_lstExpression)
            {
                whereExpr = whereExpr == null ? expr : Expression.And(whereExpr, expr);
            }
            return whereExpr == null ? null : Expression.Lambda<Func<Dto, bool>>(whereExpr, m_Parameter);
        }
    }


}
