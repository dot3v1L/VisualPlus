﻿namespace VisualPlus.Toolkit.Controls.Interactivity
{
    #region Namespace

    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Text;
    using System.Windows.Forms;

    using VisualPlus.Enumerators;
    using VisualPlus.Localization.Category;
    using VisualPlus.Localization.Descriptions;
    using VisualPlus.Managers;
    using VisualPlus.Renders;
    using VisualPlus.Structure;
    using VisualPlus.Toolkit.Components;

    #endregion

    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(TrackBar))]
    [DefaultEvent("ValueChanged")]
    [DefaultProperty("Value")]
    [Description("The Visual TrackBar")]
    [Designer(ControlManager.FilterProperties.VisualTrackBar)]
    public class VisualTrackBar : TrackBar
    {
        #region Variables

        protected Orientation _orientation;

        #endregion

        #region Variables

        private int _barThickness;
        private int _barTickSpacing;
        private bool _buttonAutoSize;
        private Border _buttonBorder;
        private ControlColorState _buttonControlColorState;
        private GraphicsPath _buttonPath;
        private Rectangle _buttonRectangle;
        private Size _buttonSize;
        private Color _buttonTextColor;
        private bool _buttonVisible;
        private int _currentUsedPos;
        private ValueDivisor _dividedValue;
        private int _fillingValue;
        private Color _foreColor;
        private Hatch _hatch;
        private int _indentHeight;
        private int _indentWidth;
        private bool _leftButtonDown;
        private bool _lineTicksVisible;
        private float _mouseStartPos;
        private MouseStates _mouseState;
        private string _prefix;
        private Color _progressColor;
        private bool _progressFilling;
        private bool _progressValueVisible;
        private bool _progressVisible;
        private VisualStyleManager _styleManager;
        private string _suffix;
        private Size _textAreaSize;
        private Color _textDisabledColor;
        private Font _textFont;
        private TextRenderingHint _textRendererHint;
        private Color _tickColor;
        private int _tickHeight;
        private Border _trackBarBorder;
        private ColorState _trackBarColor;
        private GraphicsPath _trackBarPath;
        private Rectangle _trackBarRectangle;
        private Rectangle _trackerRectangle;
        private bool _valueTicksVisible;
        private Rectangle _workingRectangle;

        #endregion

        #region Constructors

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:VisualPlus.Toolkit.Controls.Interactivity.VisualTrackBar" />
        ///     class.
        /// </summary>
        public VisualTrackBar()
        {
            SetStyle(
                ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw |
                ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor,
                true);

            UpdateStyles();
            _styleManager = new VisualStyleManager(Settings.DefaultValue.DefaultStyle);
            _trackerRectangle = Rectangle.Empty;
            _hatch = new Hatch();
            _orientation = Orientation.Horizontal;
            _buttonPath = new GraphicsPath();
            _buttonAutoSize = true;
            _buttonSize = new Size(27, 20);
            _barThickness = 10;
            _buttonVisible = true;
            _dividedValue = ValueDivisor.By1;
            _barTickSpacing = 8;
            _fillingValue = 25;
            _mouseStartPos = -1;
            _progressVisible = Settings.DefaultValue.TextVisible;
            _textRendererHint = Settings.DefaultValue.TextRenderingHint;
            _tickHeight = 4;
            _valueTicksVisible = Settings.DefaultValue.TextVisible;

            _buttonControlColorState = new ControlColorState();
            _trackBarColor = new ColorState();

            BackColor = Color.Transparent;
            DoubleBuffered = true;
            UpdateStyles();
            AutoSize = false;
            Size = new Size(200, 50);
            MinimumSize = new Size(0, 0);

            _trackBarBorder = new Border();
            _buttonBorder = new Border();

            _textRendererHint = Settings.DefaultValue.TextRenderingHint;

            UpdateTheme(Settings.DefaultValue.DefaultStyle);
        }

        public enum ValueDivisor
        {
            /// <summary>The by 1.</summary>
            By1 = 1,

            /// <summary>The by 10.</summary>
            By10 = 10,

            /// <summary>The by 100.</summary>
            By100 = 100,

            /// <summary>The by 1000.</summary>
            By1000 = 1000
        }

        #endregion

        #region Properties

        [TypeConverter(typeof(ControlColorStateConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Category(Propertys.Appearance)]
        public ControlColorState BackColorState
        {
            get
            {
                return _buttonControlColorState;
            }

            set
            {
                if (value == _buttonControlColorState)
                {
                    return;
                }

                _buttonControlColorState = value;
                Invalidate();
            }
        }

        [Category(Propertys.Layout)]
        [Description(Property.Size)]
        public int BarThickness
        {
            get
            {
                return _barThickness;
            }

            set
            {
                _barThickness = value;
                Invalidate();
            }
        }

        [Category(Propertys.Layout)]
        [Description(Property.Size)]
        public int BarTickSpacing
        {
            get
            {
                return _barTickSpacing;
            }

            set
            {
                _barTickSpacing = value;
                Invalidate();
            }
        }

        [DefaultValue(true)]
        [Category(Propertys.Behavior)]
        [Description(Property.AutoSize)]
        public bool ButtonAutoSize
        {
            get
            {
                return _buttonAutoSize;
            }

            set
            {
                _buttonAutoSize = value;
                Invalidate();
            }
        }

        [TypeConverter(typeof(BorderConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Category(Propertys.Appearance)]
        public Border ButtonBorder
        {
            get
            {
                return _buttonBorder;
            }

            set
            {
                _buttonBorder = value;
                Invalidate();
            }
        }

        [Category(Propertys.Layout)]
        [Description(Property.Size)]
        public Size ButtonSize
        {
            get
            {
                return _buttonSize;
            }

            set
            {
                _buttonSize = value;
                Invalidate();
            }
        }

        [Category(Propertys.Appearance)]
        [Description(Property.Color)]
        public Color ButtonTextColor
        {
            get
            {
                return _buttonTextColor;
            }

            set
            {
                _buttonTextColor = value;
                Invalidate();
            }
        }

        [DefaultValue(true)]
        [Category(Propertys.Behavior)]
        [Description(Property.Visible)]
        public bool ButtonVisible
        {
            get
            {
                return _buttonVisible;
            }

            set
            {
                _buttonVisible = value;
                Invalidate();
            }
        }

        [Category(Propertys.Data)]
        [Description("Experiemental: Filling Value.")]
        public int FillingValue
        {
            get
            {
                return _fillingValue;
            }

            set
            {
                _fillingValue = value;
                Invalidate();
            }
        }

        public new Font Font
        {
            get
            {
                return _textFont;
            }

            set
            {
                base.Font = value;
                _textFont = value;
                Invalidate();
            }
        }

        public new Color ForeColor
        {
            get
            {
                return _foreColor;
            }

            set
            {
                base.ForeColor = value;
                _foreColor = value;
                Invalidate();
            }
        }

        [TypeConverter(typeof(HatchConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Category(Propertys.Appearance)]
        public Hatch Hatch
        {
            get
            {
                return _hatch;
            }

            set
            {
                _hatch = value;
                Invalidate();
            }
        }

        [Category(Propertys.Layout)]
        [Description(Property.Size)]
        public int IndentHeight
        {
            get
            {
                return _indentHeight;
            }

            set
            {
                _indentHeight = value;
                Invalidate();
            }
        }

        [Category(Propertys.Layout)]
        [Description(Property.Size)]
        public int IndentWidth
        {
            get
            {
                return _indentWidth;
            }

            set
            {
                _indentWidth = value;
                Invalidate();
            }
        }

        [DefaultValue(Settings.DefaultValue.TextVisible)]
        [Category(Propertys.Appearance)]
        [Description(Property.Visible)]
        public bool LineTicksVisible
        {
            get
            {
                return _lineTicksVisible;
            }

            set
            {
                _lineTicksVisible = value;
                Invalidate();
            }
        }

        [Category(Propertys.Appearance)]
        [Description(Property.MouseState)]
        public MouseStates MouseState
        {
            get
            {
                return _mouseState;
            }

            set
            {
                _mouseState = value;
                Invalidate();
            }
        }

        [Category(Propertys.Appearance)]
        [Description(Property.Orientation)]
        public new Orientation Orientation
        {
            get
            {
                return _orientation;
            }

            set
            {
                _orientation = value;
                Size = GraphicsManager.FlipOrientationSize(_orientation, Size);
                Invalidate();
            }
        }

        [Category(Propertys.Data)]
        [Description(Property.Visible)]
        public string Prefix
        {
            get
            {
                return _prefix;
            }

            set
            {
                _prefix = value;
                Invalidate();
            }
        }

        [Category(Propertys.Appearance)]
        [Description(Property.Color)]
        public Color ProgressColor
        {
            get
            {
                return _progressColor;
            }

            set
            {
                _progressColor = value;
                Invalidate();
            }
        }

        [Category(Propertys.Appearance)]
        [Description(Property.Visible)]
        public bool ProgressFilling
        {
            get
            {
                return _progressFilling;
            }

            set
            {
                _progressFilling = value;
                Invalidate();
            }
        }

        [DefaultValue(false)]
        [Category(Propertys.Behavior)]
        [Description(Property.Visible)]
        public bool ProgressValueVisible
        {
            get
            {
                return _progressValueVisible;
            }

            set
            {
                _progressValueVisible = value;
                Invalidate();
            }
        }

        [DefaultValue(true)]
        [Category(Propertys.Behavior)]
        [Description(Property.Visible)]
        public bool ProgressVisible
        {
            get
            {
                return _progressVisible;
            }

            set
            {
                _progressVisible = value;
                Invalidate();
            }
        }

        [Category(Propertys.Data)]
        [Description(Property.Visible)]
        public string Suffix
        {
            get
            {
                return _suffix;
            }

            set
            {
                _suffix = value;
                Invalidate();
            }
        }

        [Category(Propertys.Appearance)]
        [Description(Property.Color)]
        public Color TextDisabledColor
        {
            get
            {
                return _textDisabledColor;
            }

            set
            {
                _textDisabledColor = value;
                Invalidate();
            }
        }

        [Category(Propertys.Appearance)]
        [Description(Property.TextRenderingHint)]
        public TextRenderingHint TextRendering
        {
            get
            {
                return _textRendererHint;
            }

            set
            {
                _textRendererHint = value;
                Invalidate();
            }
        }

        [Category(Propertys.Appearance)]
        [Description(Property.Color)]
        public Color TickColor
        {
            get
            {
                return _tickColor;
            }

            set
            {
                _tickColor = value;
                Invalidate();
            }
        }

        [Category(Propertys.Layout)]
        [Description(Property.Size)]
        public int TickHeight
        {
            get
            {
                return _tickHeight;
            }

            set
            {
                _tickHeight = value;
                Invalidate();
            }
        }

        [TypeConverter(typeof(BorderConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Category(Propertys.Appearance)]
        public Border TrackBar
        {
            get
            {
                return _trackBarBorder;
            }

            set
            {
                _trackBarBorder = value;
                Invalidate();
            }
        }

        [TypeConverter(typeof(ColorStateConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Category(Propertys.Appearance)]
        public ColorState TrackBarState
        {
            get
            {
                return _trackBarColor;
            }

            set
            {
                if (value == _trackBarColor)
                {
                    return;
                }

                _trackBarColor = value;
                Invalidate();
            }
        }

        [Category(Propertys.Behavior)]
        [Description(Property.ValueDivisor)]
        public ValueDivisor ValueDivision
        {
            get
            {
                return _dividedValue;
            }

            set
            {
                _dividedValue = value;
                Invalidate();
            }
        }

        [DefaultValue(Settings.DefaultValue.TextVisible)]
        [Category(Propertys.Appearance)]
        [Description(Property.Visible)]
        public bool ValueTicksVisible
        {
            get
            {
                return _valueTicksVisible;
            }

            set
            {
                _valueTicksVisible = value;
                Invalidate();
            }
        }

        #endregion

        #region Events

        /// <summary>Call the Decrement() method to decrease the value displayed by an integer you specify.</summary>
        /// <param name="value">The value to decrement.</param>
        public void Decrement(int value)
        {
            if (Value > Minimum)
            {
                Value -= value;
                if (Value < Minimum)
                {
                    Value = Minimum;
                }
            }
            else
            {
                Value = Minimum;
            }

            Invalidate();
        }

        /// <summary>Get's the formatted progress value.</summary>
        /// <returns>Formatted progress value.</returns>
        public string GetFormattedProgressValue()
        {
            var value = (float)(Value / (double)_dividedValue);
            string formattedString = $"{Prefix}{value}{Suffix}";

            return formattedString;
        }

        /// <summary>Call the Increment() method to increase the value displayed by an integer you specify.</summary>
        /// <param name="value">The value to increment.</param>
        public void Increment(int value)
        {
            if (Value < Maximum)
            {
                Value += value;
                if (Value > Maximum)
                {
                    Value = Maximum;
                }
            }
            else
            {
                Value = Maximum;
            }

            Invalidate();
        }

        /// <summary>Sets a new range value.</summary>
        /// <param name="minimumValue">The minimum.</param>
        /// <param name="maximumValue">The maximum.</param>
        public new void SetRange(int minimumValue, int maximumValue)
        {
            Minimum = minimumValue;

            if (Minimum > Value)
            {
                Value = Minimum;
            }

            Maximum = maximumValue;

            if (Maximum < Value)
            {
                Value = Maximum;
            }

            if (Maximum < Minimum)
            {
                Minimum = Maximum;
            }

            Invalidate();
        }

        public void UpdateTheme(Styles style)
        {
            VisualStyleManager _styleManager = new VisualStyleManager(style);

            ForeColor = _styleManager.FontStyle.ForeColor;

            Font = _styleManager.Font;
            _textFont = Font;
            _foreColor = _styleManager.FontStyle.ForeColor;
            _buttonTextColor = ForeColor;
            _textDisabledColor = _styleManager.FontStyle.ForeColorDisabled;

            _progressColor = _styleManager.ProgressStyle.Progress;

            _buttonControlColorState.Enabled = _styleManager.ControlStyle.Background(0);
            _buttonControlColorState.Disabled = Color.FromArgb(224, 224, 224);
            _buttonControlColorState.Hover = Color.FromArgb(224, 224, 224);
            _buttonControlColorState.Pressed = Color.Silver;

            _trackBarColor.Enabled = _styleManager.ProgressStyle.BackProgress;
            _trackBarColor.Disabled = _styleManager.ProgressStyle.ProgressDisabled;

            _hatch.BackColor = _styleManager.ProgressStyle.Hatch;
            _hatch.ForeColor = Color.FromArgb(40, _hatch.BackColor);

            _tickColor = _styleManager.ControlStyle.Line;

            _buttonBorder.Color = _styleManager.ShapeStyle.Color;
            _buttonBorder.HoverColor = _styleManager.BorderStyle.HoverColor;

            _trackBarBorder.Color = _styleManager.ShapeStyle.Color;
            _trackBarBorder.HoverColor = _styleManager.BorderStyle.HoverColor;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            var offsetValue = 0;
            Point currentPoint = new Point(e.X, e.Y);

            // TODO: Improve location accuracy
            if (_trackerRectangle.Contains(currentPoint))
            {
                if (!_leftButtonDown)
                {
                    _mouseState = MouseStates.Down;
                    _leftButtonDown = true;
                    Capture = true;
                    switch (_orientation)
                    {
                        case Orientation.Horizontal:
                            {
                                _mouseStartPos = currentPoint.X - _trackerRectangle.X;
                                Invalidate();
                                break;
                            }

                        case Orientation.Vertical:
                            {
                                _mouseStartPos = currentPoint.Y - _trackerRectangle.Y;
                                Invalidate();
                                break;
                            }
                    }
                }
            }
            else
            {
                switch (_orientation)
                {
                    case Orientation.Horizontal:
                        {
                            if ((currentPoint.X + _buttonSize.Width) / 2 >= Width - _indentWidth)
                            {
                                offsetValue = Maximum - Minimum;
                            }
                            else if ((currentPoint.X - _buttonSize.Width) / 2 <= _indentWidth)
                            {
                                offsetValue = 0;
                            }
                            else
                            {
                                offsetValue = (int)(((((currentPoint.X - _indentWidth - _buttonSize.Width) / 2) * (Maximum - Minimum)) / (Width - (2 * _indentWidth) - _buttonSize.Width)) + 0.5);
                            }

                            break;
                        }

                    case Orientation.Vertical:
                        {
                            if ((currentPoint.Y + _buttonSize.Width) / 2 >= Height - _indentHeight)
                            {
                                offsetValue = 0;
                            }
                            else if ((currentPoint.Y - _buttonSize.Width) / 2 <= _indentHeight)
                            {
                                offsetValue = Maximum - Minimum;
                            }
                            else
                            {
                                offsetValue = (int)(((((Height - currentPoint.Y - _indentHeight - _buttonSize.Width) / 2) * (Maximum - Minimum)) / (Height - (2 * _indentHeight) - _buttonSize.Width)) + 0.5);
                            }

                            break;
                        }

                    default:
                        {
                            throw new ArgumentOutOfRangeException();
                        }
                }

                int oldValue = Value;
                Value = Minimum + offsetValue;

                Invalidate();

                if (oldValue != Value)
                {
                    OnScroll(e);
                    OnValueChanged(e);
                }
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            OnEnter(e);
            _mouseState = MouseStates.Hover;
            Invalidate();
        }

        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseHover(e);
            Cursor = _orientation == Orientation.Vertical ? Cursors.SizeNS : Cursors.SizeWE;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            OnLeave(e);
            _mouseState = MouseStates.Normal;
            Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            var offsetValue = 0;
            PointF currentPoint = new PointF(e.X, e.Y);

            if (_leftButtonDown)
            {
                try
                {
                    // TODO: Improve location accuracy
                    switch (_orientation)
                    {
                        case Orientation.Horizontal:
                            {
                                if ((currentPoint.X + _buttonSize.Width) - _mouseStartPos >= Width - _indentWidth)
                                {
                                    offsetValue = Maximum - Minimum;
                                }
                                else if (currentPoint.X - _mouseStartPos <= _indentWidth)
                                {
                                    offsetValue = 0;
                                }
                                else
                                {
                                    offsetValue = (int)((((currentPoint.X - _mouseStartPos - _indentWidth) * (Maximum - Minimum)) / (Width - (2 * _indentWidth) - _buttonSize.Width)) + 0.5);
                                }

                                break;
                            }

                        case Orientation.Vertical:
                            {
                                if ((currentPoint.Y + _buttonSize.Height) / 2 >= Height - _indentHeight)
                                {
                                    offsetValue = 0;
                                }
                                else if ((currentPoint.Y + _buttonSize.Height) / 2 <= _indentHeight)
                                {
                                    offsetValue = Maximum - Minimum;
                                }
                                else
                                {
                                    offsetValue = (int)(((((((Height - currentPoint.Y) + _buttonSize.Height) / 2) - _mouseStartPos - _indentHeight) * (Maximum - Minimum)) / (Height - (2 * _indentHeight))) + 0.5);
                                }

                                break;
                            }
                    }
                }
                catch (Exception)
                {
                    // ignored
                }
                finally
                {
                    int oldValue = Value;

                    // TODO: Vertical exception is caused when trying to scroll passed the bottom
                    Value = Minimum + offsetValue;
                    Invalidate();

                    if (oldValue != Value)
                    {
                        OnScroll(e);
                        OnValueChanged(e);
                    }
                }
            }

            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            _leftButtonDown = false;
            _mouseState = MouseStates.Normal;
            Capture = false;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            graphics.Clear(Parent.BackColor);
            graphics.FillRectangle(new SolidBrush(BackColor), ClientRectangle);
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.TextRenderingHint = _textRendererHint;

            Rectangle _clientRectangle = new Rectangle(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width - 1, ClientRectangle.Height - 1);
            GraphicsPath controlGraphicsPath = VisualBorderRenderer.CreateBorderTypePath(_clientRectangle, _trackBarBorder);

            _workingRectangle = Rectangle.Inflate(_clientRectangle, -_indentWidth, -_indentHeight);

            // Set control state color
            _foreColor = Enabled ? _foreColor : _textDisabledColor;

            ConfigureTickStyle(graphics);

            DrawProgress(graphics);

            VisualBorderRenderer.DrawBorderStyle(graphics, _trackBarBorder, _trackBarPath, _mouseState);

            Size _progressValue = GraphicsManager.MeasureText(graphics, Maximum.ToString(), _textFont);

            DrawButton(graphics, _progressValue);
            DrawText(graphics, _progressValue);
        }

        protected override void OnResize(EventArgs e)
        {
            Invalidate();
        }

        protected override void OnScroll(EventArgs e)
        {
            base.OnScroll(e);
            Invalidate();
        }

        protected override void OnValueChanged(EventArgs e)
        {
            Invalidate();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var result = true;

            // Specified WM_KEYDOWN enumeration value.
            const int WM_KEYDOWN = 0x0100;

            // Specified WM_SYSKEYDOWN enumeration value.
            const int WM_SYSKEYDOWN = 0x0104;

            if ((msg.Msg == WM_KEYDOWN) || (msg.Msg == WM_SYSKEYDOWN))
            {
                switch (keyData)
                {
                    case Keys.Left:
                    case Keys.Down:
                        {
                            Decrement(SmallChange);
                            break;
                        }

                    case Keys.Right:
                    case Keys.Up:
                        {
                            Increment(SmallChange);
                            break;
                        }

                    case Keys.PageUp:
                        {
                            Increment(LargeChange);
                            break;
                        }

                    case Keys.PageDown:
                        {
                            Decrement(LargeChange);
                            break;
                        }

                    case Keys.Home:
                        {
                            Value = Maximum;
                            break;
                        }

                    case Keys.End:
                        {
                            Value = Minimum;
                            break;
                        }

                    default:
                        {
                            result = base.ProcessCmdKey(ref msg, keyData);
                            break;
                        }
                }
            }

            return result;
        }

        /// <summary>Configures the tick style.</summary>
        /// <param name="graphics">Graphics input.</param>
        private void ConfigureTickStyle(Graphics graphics)
        {
            int _currentTrackerPosition;
            Point _trackBarLocation = new Point();
            Size _trackBarSize;
            Point _trackerLocation;
            Size _trackerSize;

            // Draw tick by orientation
            if (_orientation == Orientation.Horizontal)
            {
                // Start location
                _currentUsedPos = _indentHeight;

                // Setups the location & sizing
                switch (TickStyle)
                {
                    case TickStyle.TopLeft:
                        {
                            if (_buttonVisible)
                            {
                                _trackBarLocation = new Point(0, _indentHeight + _textAreaSize.Height + _tickHeight + _barTickSpacing);
                                Size = new Size(ClientRectangle.Width, _indentHeight + _textAreaSize.Height + _tickHeight + _barTickSpacing + _barThickness + (_buttonSize.Height / 2));
                            }
                            else
                            {
                                _trackBarLocation = new Point(0, _indentHeight + _textAreaSize.Height + _tickHeight + _barTickSpacing);
                                Size = new Size(ClientRectangle.Width, _indentHeight + _textAreaSize.Height + _tickHeight + _barTickSpacing + _barThickness);
                            }

                            break;
                        }

                    case TickStyle.BottomRight:
                        {
                            if (_buttonVisible)
                            {
                                _trackBarLocation = new Point(0, _indentHeight + (_buttonSize.Height / 2));
                                Size = new Size(ClientRectangle.Width, _indentHeight + _barThickness + _barTickSpacing + _tickHeight + _textAreaSize.Height + (_textAreaSize.Height / 2));
                            }
                            else
                            {
                                _trackBarLocation = new Point(0, _indentHeight);
                                Size = new Size(ClientRectangle.Width, _indentHeight + _barThickness + _barTickSpacing + _tickHeight + _textAreaSize.Height);
                            }

                            break;
                        }

                    case TickStyle.None:
                        {
                            if (_buttonVisible)
                            {
                                _trackBarLocation = new Point(0, _indentHeight + (_buttonSize.Height / 2));
                                Size = new Size(ClientRectangle.Width, _indentHeight + _barThickness + _buttonSize.Height);
                            }
                            else
                            {
                                _trackBarLocation = new Point(0, _indentHeight);
                                Size = new Size(ClientRectangle.Width, _indentHeight + _barThickness);
                            }

                            break;
                        }

                    case TickStyle.Both:
                        {
                            int totalHeight = _indentHeight + _textAreaSize.Height + _tickHeight + _barTickSpacing + _barThickness + _barTickSpacing + _tickHeight + _textAreaSize.Height + (_textAreaSize.Height / 2);

                            _trackBarLocation = new Point(0, _indentHeight + _textAreaSize.Height + _tickHeight + _barTickSpacing);
                            Size = new Size(ClientRectangle.Width, totalHeight);

                            break;
                        }

                    default:
                        {
                            throw new ArgumentOutOfRangeException();
                        }
                }

                _trackBarSize = new Size(_workingRectangle.Width, _barThickness);
                _trackBarRectangle = new Rectangle(_trackBarLocation, _trackBarSize);

                // Get tracker position
                _currentTrackerPosition = RetrieveTrackerPosition(_workingRectangle);

                // Remember this for drawing the Tracker later
                _trackerLocation = new Point(_currentTrackerPosition, _currentUsedPos);
                _trackerSize = new Size(_buttonSize.Width, _buttonSize.Height);
                _trackerRectangle = new Rectangle(_trackerLocation, _trackerSize);

                // Draws track bar
                DrawTrackBar(graphics, _trackBarRectangle);

                // Update current position
                _currentUsedPos += _buttonSize.Height;

                // Draw value tick
                if (_valueTicksVisible)
                {
                    HorizontalStyle(graphics, _workingRectangle, false);
                }

                // Draw line tick
                if (_lineTicksVisible)
                {
                    HorizontalStyle(graphics, _workingRectangle, true);
                }
            }
            else
            {
                // Start location
                _currentUsedPos = _indentWidth;

                // Setups the location & sizing
                switch (TickStyle)
                {
                    case TickStyle.TopLeft:
                        {
                            if (_buttonVisible)
                            {
                                _trackBarLocation = new Point(_indentWidth + _textAreaSize.Width + _tickHeight + _barTickSpacing, 0);
                                Size = new Size(_indentWidth + _textAreaSize.Width + _tickHeight + _barTickSpacing + _barThickness + (_buttonSize.Width / 2), ClientRectangle.Height);
                            }
                            else
                            {
                                _trackBarLocation = new Point(_indentWidth + _textAreaSize.Width + _tickHeight + _barTickSpacing, 0);
                                Size = new Size(_indentWidth + _textAreaSize.Width + _tickHeight + _barTickSpacing + _barThickness, ClientRectangle.Height);
                            }

                            break;
                        }

                    case TickStyle.BottomRight:
                        {
                            if (_buttonVisible)
                            {
                                _trackBarLocation = new Point(_indentWidth + (_buttonSize.Width / 2), 0);
                                Size = new Size(_indentWidth + _barThickness + _barTickSpacing + _tickHeight + _textAreaSize.Width + (_buttonSize.Width / 2), ClientRectangle.Height);
                            }
                            else
                            {
                                _trackBarLocation = new Point(0, _indentWidth);
                                Size = new Size(_indentWidth + _barThickness + _barTickSpacing + _tickHeight + _textAreaSize.Width, ClientRectangle.Height);
                            }

                            break;
                        }

                    case TickStyle.None:
                        {
                            if (_buttonVisible)
                            {
                                _trackBarLocation = new Point(_indentWidth + (_buttonSize.Width / 2), _indentHeight);
                                Size = new Size(_indentWidth + _barThickness + _buttonSize.Width, ClientRectangle.Height);
                            }
                            else
                            {
                                _trackBarLocation = new Point(_indentWidth, _indentHeight);
                                Size = new Size(_indentWidth + _barThickness, ClientRectangle.Height);
                            }

                            break;
                        }

                    case TickStyle.Both:
                        {
                            int totalWidth = _indentWidth + _textAreaSize.Width + _tickHeight + _barTickSpacing + _barThickness + _barTickSpacing + _tickHeight + _textAreaSize.Width;

                            _trackBarLocation = new Point(_indentWidth + _textAreaSize.Width + _tickHeight + _barTickSpacing, 0);
                            Size = new Size(totalWidth, ClientRectangle.Height);

                            break;
                        }
                }

                _trackBarSize = new Size(_barThickness, ClientRectangle.Height - _indentHeight);
                _trackBarRectangle = new Rectangle(_trackBarLocation, _trackBarSize);

                // Get tracker position
                _currentTrackerPosition = RetrieveTrackerPosition(_workingRectangle);

                // Remember this for drawing the Tracker later
                _trackerLocation = new Point(_currentUsedPos, _workingRectangle.Bottom - _currentTrackerPosition - _buttonSize.Height);
                _trackerSize = new Size(_buttonSize.Width, _buttonSize.Height);
                _trackerRectangle = new Rectangle(_trackerLocation, _trackerSize);

                // Draw the track bar
                DrawTrackBar(graphics, _trackBarRectangle);

                // Update current position
                _currentUsedPos += _buttonSize.Height;

                // Draw value tick
                if (_valueTicksVisible)
                {
                    VerticalStyle(graphics, _workingRectangle, false);
                }

                // Draw line tick
                if (_lineTicksVisible)
                {
                    VerticalStyle(graphics, _workingRectangle, true);
                }
            }
        }

        /// <summary>Draws the button.</summary>
        /// <param name="graphics">Graphics input.</param>
        /// <param name="progressValue">The progress Value.</param>
        private void DrawButton(Graphics graphics, Size progressValue)
        {
            Point _location;
            graphics.ResetClip();

            switch (Orientation)
            {
                case Orientation.Horizontal:
                    {
                        _location = new Point(_trackerRectangle.X, (_trackBarRectangle.Top + (_barThickness / 2)) - (_buttonSize.Height / 2));
                        _buttonSize = _buttonAutoSize ? new Size(progressValue.Width, _buttonSize.Height) : new Size(_buttonSize.Width, _buttonSize.Height);
                        break;
                    }

                case Orientation.Vertical:
                    {
                        _location = new Point((_trackBarRectangle.Left + (_barThickness / 2)) - (_buttonSize.Width / 2), _trackerRectangle.Y);
                        _buttonSize = _buttonAutoSize ? new Size(_buttonSize.Width, progressValue.Height) : new Size(_buttonSize.Width, _buttonSize.Height);
                        break;
                    }

                default:
                    throw new ArgumentOutOfRangeException();
            }

            _buttonRectangle = new Rectangle(_location, _buttonSize);

            if (!_buttonVisible)
            {
                return;
            }

            Color _backColor = GraphicsManager.GetBackColorState(Enabled, _buttonControlColorState.Enabled, _buttonControlColorState.Hover, _buttonControlColorState.Pressed, _buttonControlColorState.Disabled, _mouseState);

            _buttonPath = VisualBorderRenderer.CreateBorderTypePath(_buttonRectangle, _buttonBorder);
            graphics.FillPath(new SolidBrush(_backColor), _buttonPath);

            VisualBorderRenderer.DrawBorderStyle(graphics, _buttonBorder, _buttonPath, _mouseState);
        }

        /// <summary>Draws the TrackBar progress.</summary>
        /// <param name="graphics">Graphics input.</param>
        private void DrawProgress(Graphics graphics)
        {
            if (!_progressVisible)
            {
                return;
            }

            GraphicsPath _progressPath;
            Rectangle _progressRectangle;
            Point _location;
            Size _size;

            int _barProgress;

            switch (Orientation)
            {
                case Orientation.Horizontal:
                    {
                        // Draws the progress to the middle of the button
                        _barProgress = _buttonRectangle.X + (_buttonRectangle.Width / 2);

                        _location = new Point(0, 0);
                        _size = new Size(_barProgress, Height);

                        if ((Value == Minimum) && _progressFilling)
                        {
                            _location = new Point(_barProgress, Height);
                        }

                        if ((Value == Maximum) && _progressFilling)
                        {
                            _size = new Size(_barProgress + _fillingValue, Height);
                        }

                        _progressRectangle = new Rectangle(_location, _size);
                        _progressPath = VisualBorderRenderer.CreateBorderTypePath(_progressRectangle, _trackBarBorder);
                    }

                    break;
                case Orientation.Vertical:
                    {
                        // Draws the progress to the middle of the button
                        _barProgress = _buttonRectangle.Y + (_buttonRectangle.Height / 2);

                        _location = new Point(0, _barProgress);

                        if ((Value == Minimum) && _progressFilling)
                        {
                            _location = new Point(0, _barProgress + _fillingValue);
                        }

                        if ((Value == Maximum) && _progressFilling)
                        {
                            _location = new Point(0, _barProgress - _fillingValue);
                        }

                        _size = new Size(Width, Height + _textAreaSize.Height);
                        _progressRectangle = new Rectangle(_location, _size);
                        _progressPath = VisualBorderRenderer.CreateBorderTypePath(_progressRectangle, _trackBarBorder);
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            graphics.SetClip(_trackBarPath);

            if (_barProgress > 1)
            {
                graphics.FillPath(new SolidBrush(_progressColor), _progressPath);
                VisualControlRenderer.DrawHatch(graphics, _hatch, _progressPath);
            }

            graphics.ResetClip();
        }

        /// <summary>Draws the progress text.</summary>
        /// <param name="graphics">Graphics input.</param>
        /// <param name="progressValue">The progress Value.</param>
        private void DrawText(Graphics graphics, Size progressValue)
        {
            if (!_progressValueVisible)
            {
                return;
            }

            string value = GetFormattedProgressValue();
            Point _textLocation = new Point();

            if (_buttonVisible)
            {
                _textLocation = new Point((_buttonRectangle.X + (_buttonRectangle.Width / 2)) - (progressValue.Width / 2), (_buttonRectangle.Y + (_buttonRectangle.Height / 2)) - (progressValue.Height / 2));
            }
            else
            {
                switch (Orientation)
                {
                    case Orientation.Horizontal:
                        {
                            _textLocation = new Point(_trackerRectangle.X, (_trackBarRectangle.Y + (_trackBarRectangle.Height / 2)) - (progressValue.Height / 2));
                            break;
                        }

                    case Orientation.Vertical:
                        {
                            _textLocation = new Point(_trackBarRectangle.X, _trackerRectangle.Y);
                            break;
                        }
                }
            }

            graphics.DrawString(value, _textFont, new SolidBrush(_buttonTextColor), _textLocation);
        }

        /// <summary>Draws the TrackBar.</summary>
        /// <param name="graphics">The specified graphics to draw on.</param>
        /// <param name="rectangle">Trackbar rectangle rectangle.</param>
        private void DrawTrackBar(Graphics graphics, Rectangle rectangle)
        {
            Point _trackBarLocation;
            Size _trackBarSize;

            switch (_orientation)
            {
                case Orientation.Horizontal:
                    {
                        _trackBarLocation = new Point(_indentWidth + rectangle.Left, _indentHeight + rectangle.Top);
                        _trackBarSize = new Size(rectangle.Width, rectangle.Height);
                        break;
                    }

                case Orientation.Vertical:
                    {
                        _trackBarLocation = new Point(_indentWidth + rectangle.Left, _indentHeight + rectangle.Top);
                        _trackBarSize = new Size(rectangle.Width, rectangle.Height);
                        break;
                    }

                default:
                    {
                        throw new ArgumentOutOfRangeException();
                    }
            }

            _trackBarRectangle = new Rectangle(_trackBarLocation, _trackBarSize);
            _trackBarPath = VisualBorderRenderer.CreateBorderTypePath(_trackBarRectangle, _trackBarBorder);

            Color _backColor = Enabled ? _trackBarColor.Enabled : _trackBarColor.Disabled;
            VisualBackgroundRenderer.DrawBackground(graphics, _backColor, BackgroundImage, _mouseState, _trackBarRectangle, _trackBarBorder);
        }

        /// <summary>Draws the horizontal style.</summary>
        /// <param name="graphics">The specified graphics to draw on.</param>
        /// <param name="rectangle">The working rectangle.</param>
        /// <param name="line">Toggle between drawing a tick line or the associated value.</param>
        private void HorizontalStyle(Graphics graphics, Rectangle rectangle, bool line)
        {
            Rectangle _tickRectangle;
            _currentUsedPos = _indentHeight;
            Point _location;
            Size _size;
            _textAreaSize = GraphicsManager.MeasureText(graphics, Maximum.ToString(), _textFont);

            if (line)
            {
                if ((TickStyle == TickStyle.TopLeft) || (TickStyle == TickStyle.Both))
                {
                    // Retrieve tick barRectangle
                    _location = new Point(rectangle.Left, _currentUsedPos + _textAreaSize.Height);
                    _size = new Size(rectangle.Width, _tickHeight);
                    _tickRectangle = new Rectangle(_location, _size);

                    // Move next tick area
                    _currentUsedPos += _tickHeight;
                    GraphicsManager.DrawTickLine(graphics, _tickRectangle, TickFrequency, Minimum, Maximum, _tickColor, _orientation);
                }

                if ((TickStyle == TickStyle.BottomRight) || (TickStyle == TickStyle.Both))
                {
                    // Retrieve tick barRectangle
                    _location = new Point(rectangle.Left, _trackBarRectangle.Bottom + _barTickSpacing);
                    _size = new Size(rectangle.Width, _tickHeight);
                    _tickRectangle = new Rectangle(_location, _size);

                    // Enlarge tick barRectangle
                    _tickRectangle.Inflate(-_buttonSize.Width / 2, 0);

                    // Move next tick area
                    _currentUsedPos += _tickHeight;
                    GraphicsManager.DrawTickLine(graphics, _tickRectangle, TickFrequency, Minimum, Maximum, _tickColor, _orientation);
                }
            }
            else
            {
                if ((TickStyle == TickStyle.TopLeft) || (TickStyle == TickStyle.Both))
                {
                    // Retrieve text barRectangle
                    _location = new Point(rectangle.Left, _currentUsedPos);
                    _size = new Size(rectangle.Width, _textAreaSize.Height);
                    _tickRectangle = new Rectangle(_location, _size);

                    // Enlarge text barRectangle
                    _tickRectangle.Inflate(-_buttonSize.Width / 2, 0);

                    // Move next text area
                    _currentUsedPos += _tickHeight;
                    GraphicsManager.DrawTickTextLine(graphics, _tickRectangle, TickFrequency, Minimum, Maximum, _foreColor, _textFont, _orientation);
                }

                if ((TickStyle == TickStyle.BottomRight) || (TickStyle == TickStyle.Both))
                {
                    // Retrieve text barRectangle
                    _location = new Point(rectangle.Left, _trackBarRectangle.Y + _trackBarRectangle.Height + _trackBarBorder.Rounding + _barTickSpacing + _tickHeight + _currentUsedPos);
                    _size = new Size(rectangle.Width, _tickHeight);
                    _tickRectangle = new Rectangle(_location, _size);

                    // Enlarge text barRectangle
                    _tickRectangle.Inflate(-_buttonSize.Width / 2, 0);

                    // Move next text area
                    _currentUsedPos += _tickHeight;
                    GraphicsManager.DrawTickTextLine(graphics, _tickRectangle, TickFrequency, Minimum, Maximum, _foreColor, _textFont, _orientation);
                }
            }
        }

        /// <summary>Returns the tracker variable position.</summary>
        /// <param name="rectangle">The working rectangle.</param>
        /// <returns>Tracker position.</returns>
        private int RetrieveTrackerPosition(Rectangle rectangle)
        {
            int _currentPosition;

            switch (Orientation)
            {
                case Orientation.Horizontal:
                    {
                        if (Maximum == Minimum)
                        {
                            _currentPosition = rectangle.Left;
                        }
                        else
                        {
                            _currentPosition = (((rectangle.Width - _buttonSize.Width) * (Value - Minimum)) / (Maximum - Minimum)) + rectangle.Left;
                        }

                        break;
                    }

                case Orientation.Vertical:
                    {
                        if (Maximum == Minimum)
                        {
                            _currentPosition = rectangle.Top;
                        }
                        else
                        {
                            _currentPosition = ((rectangle.Height - _buttonSize.Height) * (Value - Minimum)) / (Maximum - Minimum);
                        }

                        break;
                    }

                default:
                    {
                        throw new ArgumentOutOfRangeException();
                    }
            }

            return _currentPosition;
        }

        /// <summary>Draws the vertical style.</summary>
        /// <param name="graphics">The specified graphics to draw on.</param>
        /// <param name="rectangle">The working rectangle.</param>
        /// <param name="line">Toggle between drawing a tick line or the associated value.</param>
        private void VerticalStyle(Graphics graphics, Rectangle rectangle, bool line)
        {
            Rectangle _tickRectangle;
            _currentUsedPos = _indentWidth;
            Point _location;
            Size _size;
            _textAreaSize = GraphicsManager.MeasureText(graphics, Maximum.ToString(), _textFont);

            if (line)
            {
                if ((TickStyle == TickStyle.TopLeft) || (TickStyle == TickStyle.Both))
                {
                    // Retrieve tick barRectangle
                    _location = new Point(_currentUsedPos + _textAreaSize.Width, _textAreaSize.Height / 2);
                    _size = new Size(_tickHeight, _trackBarRectangle.Height - _textAreaSize.Height);
                    _tickRectangle = new Rectangle(_location, _size);

                    // Move next tick area
                    _currentUsedPos += _tickHeight;
                    GraphicsManager.DrawTickLine(graphics, _tickRectangle, TickFrequency, Minimum, Maximum, _tickColor, _orientation);
                }

                if ((TickStyle == TickStyle.BottomRight) || (TickStyle == TickStyle.Both))
                {
                    // Retrieve tick barRectangle
                    _location = new Point(_trackBarRectangle.Right + _barTickSpacing, rectangle.Top + (_textAreaSize.Height / 2));
                    _size = new Size(_tickHeight, _trackBarRectangle.Height - _textAreaSize.Height);
                    _tickRectangle = new Rectangle(_location, _size);

                    // Move next tick area
                    _currentUsedPos += _tickHeight;
                    GraphicsManager.DrawTickLine(graphics, _tickRectangle, TickFrequency, Minimum, Maximum, _tickColor, _orientation);
                }
            }
            else
            {
                if ((TickStyle == TickStyle.TopLeft) || (TickStyle == TickStyle.Both))
                {
                    // Retrieve text barRectangle
                    _location = new Point(_currentUsedPos, _textAreaSize.Height / 2);
                    _size = new Size(_textAreaSize.Width, _trackBarRectangle.Height - _textAreaSize.Height);
                    _tickRectangle = new Rectangle(_location, _size);

                    // Move next text area
                    _currentUsedPos += _tickHeight;
                    GraphicsManager.DrawTickTextLine(graphics, _tickRectangle, TickFrequency, Minimum, Maximum, _foreColor, _textFont, _orientation);
                }

                if ((TickStyle == TickStyle.BottomRight) || (TickStyle == TickStyle.Both))
                {
                    // Retrieve text barRectangle
                    _location = new Point(_trackBarRectangle.Right + _barTickSpacing + _tickHeight, rectangle.Top + (_textAreaSize.Height / 2));
                    _size = new Size(_textAreaSize.Width, rectangle.Height - _textAreaSize.Height);
                    _tickRectangle = new Rectangle(_location, _size);

                    // Move next text area
                    _currentUsedPos += _tickHeight;
                    GraphicsManager.DrawTickTextLine(graphics, _tickRectangle, TickFrequency, Minimum, Maximum, _foreColor, _textFont, _orientation);
                }
            }
        }

        #endregion
    }
}