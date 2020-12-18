using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.DirectoryServices.ActiveDirectory;
using System.Text;

namespace Improve.Module.Plan.Models
{
    public enum SchedulyType
    {
        Week,
        Month
    }

    public abstract class Schedule:BindableBase
    {
        /// <summary>
        /// 代表一天24小时
        /// </summary>
        public IEnumerable<HourOfDay> Hours
        {
            get
            {
                for (int i = 0; i < 24; i++)
                {
                    yield return (HourOfDay)i;
                }
            }
        }
        public ObservableCollection<Todo> Todos { get;private set; }

        public void AddTodo(Todo todo)
        {
            if (Todos==null)
            {
                Todos = new ObservableCollection<Todo>();
            }
            Todos.Add(todo);
        }
    }
}
