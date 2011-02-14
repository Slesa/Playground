using System;
using Caliburn.Micro;

namespace MediaOwl.Core
{
    /// <summary>
    /// This class is used to identify naming conventions, that are not part of the CM-Framework.
    /// </summary>
    public static class NamingConventions
    {
        public static string ViewModelPostFix = "ViewModel";
        public static string HomePostFix = "Home";
        public static string SinglePostFix = "Single";

        /// <summary>
        /// Gets the name of the MainScreen of a given screen.
        /// </summary>
        /// <param name="screen">The screen</param>
        /// <param name="returnQualified">If true, the returned string will be the qualified name</param>
        /// <returns>The MainScreen-Name</returns>
        public static string GetMainScreenName(this IScreen screen, bool returnQualified = false)
        {
            return returnQualified
                       ? GetQualifiedViewModelName(screen, string.Empty, ViewModelPostFix)
                       : GetViewModelName(screen, string.Empty, ViewModelPostFix);
        }

        /// <summary>
        /// Gets the name of the HomeScreen of a given screen.
        /// </summary>
        /// <param name="screen">The screen</param>
        /// <param name="returnQualified">If true, the returned string will be the qualified name</param>
        /// <returns>The HomeScreen-Name</returns>
        public static string GetHomeScreenName(this IScreen screen, bool returnQualified = false)
        {
            return returnQualified
                       ? GetQualifiedViewModelName(screen, HomePostFix, ViewModelPostFix)
                       : GetViewModelName(screen, HomePostFix, ViewModelPostFix);
        }

        /// <summary>
        /// Gets the name of the SingleScreen of a given screen.
        /// </summary>
        /// <param name="screen">The screen</param>
        /// <param name="returnQualified">If true, the returned string will be the qualified name</param>
        /// <returns>The SingleScreen-Name</returns>
        public static string GetSingleScreenName(this IScreen screen, bool returnQualified = false)
        {
            return returnQualified
                ? GetQualifiedViewModelName(screen, SinglePostFix, ViewModelPostFix)
                : GetViewModelName(screen, SinglePostFix, ViewModelPostFix);
        }

        /// <summary>
        /// Gets the name of a ViewModel. 
        /// </summary>
        /// <param name="screen">The screen</param>
        /// <param name="targetPostFix">Either <see cref="HomePostFix"/> or <see cref="SinglePostFix"/></param>
        /// <param name="viewModelPostFix">The <see cref="ViewModelPostFix"/></param>
        /// <returns>The ViewModel-Name</returns>
        private static string GetViewModelName(IScreen screen, string targetPostFix, string viewModelPostFix)
        {
            string s = screen.GetType().Name;
            string postFix = targetPostFix + viewModelPostFix;

            if (s.EndsWith(SinglePostFix + ViewModelPostFix))
                return s.Replace(SinglePostFix + ViewModelPostFix, postFix);

            if (s.EndsWith(HomePostFix + ViewModelPostFix))
                return s.Replace(HomePostFix + ViewModelPostFix, postFix);

            if (s.EndsWith(ViewModelPostFix))
                return s.Replace(ViewModelPostFix, postFix);

            return string.Empty;
        }

        /// <summary>
        /// Gets the fully qualified name of a ViewModel. Just adds the AssemblyQualifiedName to the <see cref="GetViewModelName"/>.
        /// </summary>
        /// <param name="screen">The screen</param>
        /// <param name="targetPostFix">Either <see cref="HomePostFix"/> or <see cref="SinglePostFix"/></param>
        /// <param name="viewModelPostFix">The <see cref="ViewModelPostFix"/></param>
        /// <returns>The fully qualified ViewModel-Name</returns>
        private static string GetQualifiedViewModelName(IScreen screen, string targetPostFix, string viewModelPostFix)
        {
            string qualifiedName = screen.GetType().AssemblyQualifiedName;
            return qualifiedName.Replace(
                screen.GetType().Name,
                GetViewModelName(screen, targetPostFix, viewModelPostFix));
        }


        /// <summary>
        /// Gets the Type of the MainScreen of a given screen.
        /// </summary>
        /// <remarks>Uses <see cref="GetMainScreenName"/></remarks>
        /// <param name="screen">The screen</param>
        /// <returns>The MainScreen-Type</returns>
        public static Type GetMainScreenType(this IScreen screen)
        {
            return Type.GetType(GetMainScreenName(screen, true));
        }

        /// <summary>
        /// Gets the Type of the HomeScreen of a given screen.
        /// </summary>
        /// <remarks>Uses <see cref="GetHomeScreenName"/></remarks>
        /// <param name="screen">The screen</param>
        /// <returns>The HomeScreen-Type</returns>
        public static Type GetHomeScreenType(this IScreen screen)
        {
            return Type.GetType(GetHomeScreenName(screen, true));
        }

        /// <summary>
        /// Gets the Type of the SingleScreen of a given screen.
        /// </summary>
        /// <remarks>Uses <see cref="GetSingleScreenName"/></remarks>
        /// <param name="screen">The screen</param>
        /// <returns>The SingleScreen-Type</returns>
        public static Type GetSingleScreenType(this IScreen screen)
        {
            string s = GetSingleScreenName(screen, true);
            Type t = Type.GetType(s);
            return t;
        }

        /// <summary>
        /// Indicates, if the given screen is a MainScreen.
        /// </summary>
        /// <param name="screen">The screen</param>
        /// <returns>True if it is a MainScreen, else false</returns>
        public static bool IsMainScreen(this IScreen screen)
        {
            string s = screen.GetType().Name;
            if (!s.EndsWith(SinglePostFix + ViewModelPostFix) &&
                !s.EndsWith(HomePostFix + ViewModelPostFix) &&
                s.EndsWith(ViewModelPostFix))
                return true;
            return false;
        }

        /// <summary>
        /// Indicates, if the given screen is a HomeScreen.
        /// </summary>
        /// <param name="screen">The screen</param>
        /// <returns>True if it is a HomeScreen, else false</returns>
        public static bool IsHomeScreen(this IScreen screen)
        {
            string s = screen.GetType().Name;
            if (!s.EndsWith(SinglePostFix + ViewModelPostFix) &&
                s.EndsWith(HomePostFix + ViewModelPostFix) &&
                s.EndsWith(ViewModelPostFix))
                return true;
            return false;
        }

        /// <summary>
        /// Indicates, if the given screen is a SingleScreen.
        /// </summary>
        /// <param name="screen">The screen</param>
        /// <returns>True if it is a SingleScreen, else false</returns>
        public static bool IsSingleScreen(this IScreen screen)
        {
            string s = screen.GetType().Name;
            if (s.EndsWith(SinglePostFix + ViewModelPostFix) &&
                !s.EndsWith(HomePostFix + ViewModelPostFix) &&
                s.EndsWith(ViewModelPostFix))
                return true;
            return false;
        }
    }
}