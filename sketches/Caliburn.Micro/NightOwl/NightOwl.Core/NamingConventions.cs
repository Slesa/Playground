using System;
using Caliburn.Micro;

namespace NightOwl.Core
{
    public static class NamingConventions
    {
        public static string ViewModelPostFix = "ViewModel";
        public static string HomePostFix = "Home";
        public static string SinglePostFix = "Single";

        public static string GetMainScreenName(this IScreen screen, bool returnQualified = false)
        {
            return returnQualified
                       ? GetQualifiedViewModelName(screen, string.Empty, ViewModelPostFix)
                       : GetViewModelName(screen, string.Empty, ViewModelPostFix);
        }

        public static string GetHomeScreenName(this IScreen screen, bool returnQualified = false)
        {
            return returnQualified
                       ? GetQualifiedViewModelName(screen, HomePostFix, ViewModelPostFix)
                       : GetViewModelName(screen, HomePostFix, ViewModelPostFix);
        }

        public static string GetSingleScreenName(this IScreen screen, bool returnQualified = false)
        {
            return returnQualified
                ? GetQualifiedViewModelName(screen, SinglePostFix, ViewModelPostFix)
                : GetViewModelName(screen, SinglePostFix, ViewModelPostFix);
        }

        private static string GetViewModelName(IScreen screen, string targetPostFix, string viewModelPostFix)
        {
            var s = screen.GetType().Name;
            var postFix = targetPostFix + viewModelPostFix;

            if (s.EndsWith(SinglePostFix + ViewModelPostFix))
                return s.Replace(SinglePostFix + ViewModelPostFix, postFix);

            if (s.EndsWith(HomePostFix + ViewModelPostFix))
                return s.Replace(HomePostFix + ViewModelPostFix, postFix);

            if (s.EndsWith(ViewModelPostFix))
                return s.Replace(ViewModelPostFix, postFix);

            return string.Empty;
        }

        private static string GetQualifiedViewModelName(IScreen screen, string targetPostFix, string viewModelPostFix)
        {
            var qualifiedName = screen.GetType().AssemblyQualifiedName;
            if (qualifiedName == null) throw new ArgumentException("No qualified name", "screen");
            return qualifiedName.Replace(
                screen.GetType().Name,
                GetViewModelName(screen, targetPostFix, viewModelPostFix));
        }

        public static Type GetMainScreenType(this IScreen screen)
        {
            return Type.GetType(GetMainScreenName(screen, true));
        }

        public static Type GetHomeScreenType(this IScreen screen)
        {
            return Type.GetType(GetHomeScreenName(screen, true));
        }

        public static Type GetSingleScreenType(this IScreen screen)
        {
            var s = GetSingleScreenName(screen, true);
            var t = Type.GetType(s);
            return t;
        }

        public static bool IsMainScreen(this IScreen screen)
        {
            var s = screen.GetType().Name;
            return !s.EndsWith(SinglePostFix + ViewModelPostFix) &&
                   !s.EndsWith(HomePostFix + ViewModelPostFix) &&
                   s.EndsWith(ViewModelPostFix);
        }

        public static bool IsHomeScreen(this IScreen screen)
        {
            var s = screen.GetType().Name;
            return !s.EndsWith(SinglePostFix + ViewModelPostFix) &&
                   s.EndsWith(HomePostFix + ViewModelPostFix) &&
                   s.EndsWith(ViewModelPostFix);
        }

        public static bool IsSingleScreen(this IScreen screen)
        {
            var s = screen.GetType().Name;
            return s.EndsWith(SinglePostFix + ViewModelPostFix) &&
                   !s.EndsWith(HomePostFix + ViewModelPostFix) &&
                   s.EndsWith(ViewModelPostFix);
        }
    }
}
