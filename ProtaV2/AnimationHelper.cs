using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace ProtaV2
{
    internal class AnimationHelper
    {
        /// <summary>
        /// Animates form borders based on input
        /// </summary>
        public static void AnimateImageOpactiy(double fromVal, double toVal, float seconds, DependencyObject target)
        {
            Duration duration = new Duration(TimeSpan.FromSeconds(seconds));

            // Fade anim
            DoubleAnimation opactiyAnimation = new DoubleAnimation();
            opactiyAnimation.From = fromVal;
            opactiyAnimation.To = toVal;
            opactiyAnimation.Duration = duration;


            PropertyPath opacityPath = new PropertyPath("(Image.Opacity)");

            Storyboard opactiyAnimStory = new Storyboard();
            Storyboard.SetTarget(opactiyAnimation, target);

            Storyboard.SetTargetProperty(opactiyAnimation, opacityPath);
            opactiyAnimStory.Children.Add(opactiyAnimation);
            opactiyAnimStory.Begin();
        }

        public static void AnimatePageOpactiy(double fromVal, double toVal, float seconds, DependencyObject target)
        {
            Duration duration = new Duration(TimeSpan.FromSeconds(seconds));

            // Fade anim
            DoubleAnimation opactiyAnimation = new DoubleAnimation();
            opactiyAnimation.From = fromVal;
            opactiyAnimation.To = toVal;
            opactiyAnimation.Duration = duration;


            PropertyPath opacityPath = new PropertyPath("(Page.Opacity)");

            Storyboard opactiyAnimStory = new Storyboard();
            Storyboard.SetTarget(opactiyAnimation, target);

            Storyboard.SetTargetProperty(opactiyAnimation, opacityPath);
            opactiyAnimStory.Children.Add(opactiyAnimation);
            opactiyAnimStory.Begin();
        }

        public static void AnimateStackPanelOpactiy(double fromVal, double toVal, float seconds, DependencyObject target)
        {
            Duration duration = new Duration(TimeSpan.FromSeconds(seconds));

            // Fade anim
            DoubleAnimation opactiyAnimation = new DoubleAnimation();
            opactiyAnimation.From = fromVal;
            opactiyAnimation.To = toVal;
            opactiyAnimation.Duration = duration;


            PropertyPath opacityPath = new PropertyPath("(StackPanel.Opacity)");

            Storyboard opactiyAnimStory = new Storyboard();
            Storyboard.SetTarget(opactiyAnimation, target);

            Storyboard.SetTargetProperty(opactiyAnimation, opacityPath);
            opactiyAnimStory.Children.Add(opactiyAnimation);
            opactiyAnimStory.Begin();
        }
    }
}
