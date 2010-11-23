namespace Caliburn.Micro.Recipes.Filters.Framework {
    using System;
    using System.ComponentModel;
    using System.Linq;

    /// <summary>
    /// Updates the availability of the action (thus updating the UI)
    /// </summary>
    /// <example>
    /// <code>
    /// [Dependencies("MyProperty", "MyOtherProperty")]
    /// public void DoAction() { ... }
    /// public bool CanDoAction() { return MyProperty > 0 && MyOtherProperty < 1; }
    /// </code>
    /// </example>
    public class DependenciesAttribute : Attribute, IContextAware {
        ActionExecutionContext context;
        INotifyPropertyChanged target;

        public DependenciesAttribute(params string[] propertyNames) {
            PropertyNames = propertyNames ?? new string[] {};
        }

        public string[] PropertyNames { get; private set; }
        public int Priority { get; set; }

        public void MakeAwareOf(ActionExecutionContext context) {
            this.context = context;
            target = context.Target as INotifyPropertyChanged;

            if(target != null)
                target.PropertyChanged += TargetPropertyChanged;
        }

        public void Dispose() {
            if(target != null)
                target.PropertyChanged -= TargetPropertyChanged;
            target = null;
        }

        void TargetPropertyChanged(object sender, PropertyChangedEventArgs e) {
            if(string.IsNullOrEmpty(e.PropertyName) || PropertyNames.Contains(e.PropertyName))
                Execute.OnUIThread(() => { context.Message.UpdateAvailability(); });
        }
    }
}