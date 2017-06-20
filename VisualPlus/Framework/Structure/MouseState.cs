﻿namespace VisualPlus.Framework.Structure
{
    #region Namespace

    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Windows.Forms;

    using VisualPlus.Controls;

    #endregion

    public enum MouseStates
    {
        /// <summary>Normal state.</summary>
        Normal,

        /// <summary>Hover state.</summary>
        Hover,

        /// <summary>Down state.</summary>
        Down
    }

    [Description("The state of the mouse on the control.")]
    public class MouseState
    {
        #region Variables

        private MouseStates mouseState = MouseStates.Normal;

        #endregion

        #region Constructors

        public MouseState(Control control)
        {
            control.MouseDown += OnMouseDown;
            control.MouseEnter += OnMouseEnter;
            control.MouseLeave += OnMouseLeave;
            control.MouseUp += OnMouseUp;

            // Specific controls might need to ignore some events
            //if (!(control is VisualCheckBox))
            //{
            //    // Add here
            //}

            //if (control is VisualNumericUpDown)
            //{
            //    control.MouseEnter += OnMouseEnter;
            //    control.MouseLeave += OnMouseLeave;

            // //   control.Invalidate();

            //    // TODO: Doesn't seem to be registering the OnMouseLeave() event Invalidate() after on the control properly.
            //}

            //if (control is VisualRichTextBox)
            //{
            //    control.Enter += OnMouseEnter;
            //  //  control.Leave += OnMouseLeave;
            //}

            //if (control is VisualTextBox)
            //{
            //    control.Enter += OnMouseEnter;
            //   // control.Leave += OnMouseLeave;
            //}
        }

        #endregion

        #region Properties

        [NotifyParentProperty(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [Description("The state of the control.")]
        public MouseStates State
        {
            get
            {
                return mouseState;
            }

            set
            {
                mouseState = value;
            }
        }

        #endregion

        #region Events

        protected virtual void OnMouseDown(object sender, MouseEventArgs e)
        {
            mouseState = MouseStates.Down;
        }

        protected virtual void OnMouseEnter(object sender, EventArgs e)
        {
            mouseState = MouseStates.Hover;
        }

        protected virtual void OnMouseLeave(object sender, EventArgs e)
        {
            mouseState = MouseStates.Normal;
        }

        protected virtual void OnMouseUp(object sender, MouseEventArgs e)
        {
            mouseState = MouseStates.Hover;
        }

        #endregion
    }
}