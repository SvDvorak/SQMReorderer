using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace SQMReorderer.Gui.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void Set<TProperty>(object value, Expression<Func<TProperty>> propertyLambda, Action propertySetter)
        {
            propertySetter();

            var propertyName = GetPropertyName(propertyLambda);

            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private string GetPropertyName<TProperty>(Expression<Func<TProperty>> propertyLambda)
        {
            var member = propertyLambda.Body as MemberExpression;
            if (member == null)
            {
                throw new ArgumentException(
                    string.Format("Expression '{0}' refers to a method, not a property.", propertyLambda));
            }

            var propertyInfo = member.Member as PropertyInfo;
            if (propertyInfo == null)
            {
                throw new ArgumentException(
                    string.Format("Expression '{0}' refers to a field, not a property.", propertyLambda));
            }

            return propertyInfo.Name;
        }

        protected void FirePropertyChanged<TProperty>(Expression<Func<TProperty>> propertyLambda)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(GetPropertyName(propertyLambda)));
            }
        }
    }
}