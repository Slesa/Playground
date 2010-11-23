namespace Caliburn.Micro.Recipes.Filters.ViewModels {
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Windows;
    using Framework;
    using MessageBoxResult = Results.MessageBoxResult;

    public class CalculatorViewModel : Screen, ICanBeBusy {
        double a;
        double b;
        bool isBusy;
        double result;

        public double A {
            get { return a; }
            set {
                a = value;
                NotifyOfPropertyChange(() => A);
                NotifyOfPropertyChange(() => CanDivide);
            }
        }

        public double B {
            get { return b; }
            set {
                b = value;
                NotifyOfPropertyChange(() => B);
                NotifyOfPropertyChange(() => CanDivide);
            }
        }

        public double Result {
            get { return result; }
            set {
                result = value;
                NotifyOfPropertyChange(() => Result);
            }
        }

        public bool CanDivide {
            get { return B != 0; }
        }

        public bool IsBusy {
            get { return isBusy; }
            set {
                isBusy = value;
                NotifyOfPropertyChange(() => IsBusy);
            }
        }

        public bool Rescue(Exception ex) {
            MessageBox.Show(ex.ToString());
            return true;
        }

        public void Divide() {
            Result = A / B;
        }

        [DoNotExecute]
        public void SkippedDivide() {
            Result = A / B;
        }

        [SetBusy, Async, Preview("CanDivide"), Dependencies("B")]
        public void VerySlowDivide() {
            Thread.Sleep(2000);
            Result = A / B;
        }

        [Rescue]
        public void ThrowingDivide() {
            throw new NotImplementedException();
        }

        [Preview("CanDivide")]
        public void NotUpdatedDivide() {}

        [Preview("CanDivide"), Dependencies("B")]
        public IEnumerable<IResult> ChattyDivide() {
            yield return new MessageBoxResult("I'm about to execute division");
            yield return new MessageBoxResult("Let me think a bit more");

            Result = A / B;

            yield return new MessageBoxResult("I'm done");
        }
    }
}