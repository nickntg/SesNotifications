using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace SesNotifications.DataAccess.Repositories
{
    public static class CriteriaHelper
    {
        public static ICriteria AddIfNotEmpty(this ICriteria criteria, string value, string propertyName)
        {
            if (string.IsNullOrEmpty(value))
            {
                return criteria;
            }

            return criteria.Add(Restrictions.InsensitiveLike(propertyName, value));
        }
    }
}
