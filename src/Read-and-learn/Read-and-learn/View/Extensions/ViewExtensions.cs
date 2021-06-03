using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Read_and_learn.View.Extensions
{
    /// <summary>
    /// Extensions for <see cref="VisualElement"/>.
    /// </summary>
    public static class ViewExtensions
    {
        /// <summary>
        /// Change color from <paramref name="fromColor"/> to <paramref name="toColor"/>
        /// via target <paramref name="callback"/> with setted animation length via <paramref name="length"/>.
        /// </summary>
        /// <param name="self">Target <see cref="VisualElement"/></param>
        /// <param name="fromColor">From which color should be changed</param>
        /// <param name="toColor">Target color to change</param>
        /// <param name="callback">Target callback</param>
        /// <param name="length">Animation length in tics</param>
        /// <param name="easing">Target <see cref="Easing"/></param>
        public static Task<bool> ColorTo(this VisualElement self, Color fromColor, Color toColor, Action<Color> callback, uint length = 250, Easing easing = null)
        {
            Func<double, Color> transform = (t) =>
              Color.FromRgba(fromColor.R + t * (toColor.R - fromColor.R),
                             fromColor.G + t * (toColor.G - fromColor.G),
                             fromColor.B + t * (toColor.B - fromColor.B),
                             fromColor.A + t * (toColor.A - fromColor.A));

            return _ColorAnimation(self, "ColorTo", transform, callback, length, easing);
        }

        /// <summary>
        /// Cancel animation for target <paramref name="self"/>.
        /// </summary>
        /// <param name="self">Target <see cref="VisualElement"/></param>
        public static void CancelAnimation(this VisualElement self)
            => self.AbortAnimation("ColorTo");

        private static Task<bool> _ColorAnimation(VisualElement element, string name, Func<double, Color> transform, Action<Color> callback, uint length, Easing easing)
        {
            easing = easing ?? Easing.Linear;
            var taskCompletionSource = new TaskCompletionSource<bool>();

            element.Animate(name, transform, callback, 16, length, easing, (v, c) => taskCompletionSource.SetResult(c));
            return taskCompletionSource.Task;
        }
    }
}
