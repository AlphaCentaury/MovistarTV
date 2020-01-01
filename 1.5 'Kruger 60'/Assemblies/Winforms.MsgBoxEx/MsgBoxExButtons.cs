// ==============================================================================
// 
//   Copyright (C) 2014-2020, GitHub/Codeplex user AlphaCentaury
//   All rights reserved.
// 
//     See 'LICENSE.MD' file (or 'license.txt' if missing) in the project root
//     for complete license information.
// 
//   http://www.alphacentaury.org/movistartv
//   https://github.com/AlphaCentaury
// 
// ==============================================================================

using System.Windows.Forms;

namespace AlphaCentaury.WindowsForms.MsgBoxEx
{
    /// <summary>
    ///     Specifies constants defining which buttons to display on a AlphaCentaury.Winforms.MsgBoxEx.
    /// </summary>
    /// <remarks>Compatible with MessageBoxButtons constants</remarks>
    public enum MsgBoxExButtons
    {
        #region "Inherit" from MessageBoxButtons
        /// <summary>
        ///     The extended message box contains an OK button.
        /// </summary>
        OK = MessageBoxButtons.OK,

        /// <summary>
        ///     The extended message box contains OK and Cancel buttons.
        /// </summary>
        OKCancel = MessageBoxButtons.OKCancel,

        /// <summary>
        ///     The extended message box contains Abort, Retry, and Ignore buttons.
        /// </summary>
        AbortRetryIgnore = MessageBoxButtons.AbortRetryIgnore,

        /// <summary>
        ///     The extended message box contains Yes, No, and Cancel buttons.
        /// </summary>
        YesNoCancel = MessageBoxButtons.YesNoCancel,

        /// <summary>
        ///     The extended message box contains Yes and No buttons.
        /// </summary>
        YesNo = MessageBoxButtons.YesNo,

        /// <summary>
        ///     The extended message box contains Retry and Cancel buttons.
        /// </summary>
        RetryCancel = MessageBoxButtons.RetryCancel,
        #endregion

        #region New constants
        /// <summary>
        ///     The extended message box contains user defined buttons.
        /// </summary>
        Custom = -100,
        #endregion
    } // enum MsgBoxExButtons
} // namespace
