using System.Windows;

namespace dotnetpro.WPF.TableReport
{
    public class PageDefinition
    {

        #region Page sizes

        /// <summary>
        /// PageSize in DIUs
        /// </summary>
        public Size PageSize
        {
            get { return _PageSize; }
            set { _PageSize = value; }
        }
        private Size _PageSize = new Size(793.5987, 1122.3987); // Default: A4

        /// <summary>
        /// Margins
        /// </summary>
        public Thickness Margins
        {
            get { return _Margins; }
            set { _Margins = value; }
        }
        private Thickness _Margins = new Thickness(30); // Default: 1" margins


        /// <summary>
        /// Space reserved for the header in DIUs
        /// </summary>
        public double HeaderHeight
        {
            get { return _HeaderHeight; }
            set { _HeaderHeight = value; }
        }
        private double _HeaderHeight;

        /// <summary>
        /// Space reserved for the footer in DIUs
        /// </summary>
        public double FooterHeight
        {
            get { return _FooterHeight; }
            set { _FooterHeight = value; }
        }
        private double _FooterHeight;

        #endregion



        #region Some convenient helper properties

        internal Size ContentSize
        {
            get
            {
                return PageSize.Subtract(new Size(
                    Margins.Left + Margins.Right,
                    Margins.Top + Margins.Bottom + HeaderHeight + FooterHeight
                ));
            }
        }

        internal Point ContentOrigin
        {
            get
            {
                return new Point(
                    Margins.Left,
                    Margins.Top + HeaderRect.Height
                );
            }
        }

        internal Rect HeaderRect
        {
            get
            {
                return new Rect(
                    Margins.Left, Margins.Top,
                    ContentSize.Width, HeaderHeight
                );
            }
        }

        internal Rect FooterRect
        {
            get
            {
                return new Rect(
                    Margins.Left, ContentOrigin.Y + ContentSize.Height,
                    ContentSize.Width, FooterHeight
                );
            }
        }

        #endregion

    }
}
