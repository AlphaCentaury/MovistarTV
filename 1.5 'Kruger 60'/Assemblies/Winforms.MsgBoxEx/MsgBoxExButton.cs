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

namespace AlphaCentaury.WindowsForms.MsgBoxEx
{
    /// <summary>
    ///     Specifies constants defining the default button on a AlphaCentaury.Winforms.MsgBoxEx.
    /// </summary>
    /// <remarks>Compatible with MessageBoxDefaultButton constants</remarks>

    public enum MsgBoxExButton
    {
        #region "Inherit" from MessageBoxDefaultButton
        /// <summary>
        //     The first button on the extended message box.
        /// </summary>
        Button1 = 0,

        /// <summary>
        //     The second button on the extended message box.
        /// </summary>
        Button2 = 0x100,

        /// <summary>
        //     The third button on the extended message box.
        /// </summary>
        Button3 = 0x200,
        #endregion

        #region New constants
        /// <summary>
        //     The fourth button on the extended message box.
        /// </summary>
        Button4 = 0x1000,

        /// <summary>
        //     The fifth button on the extended message box.
        /// </summary>
        Button5 = 0x2000,

        /// <summary>
        //     No button.
        /// </summary>
        None = 0x8000,
        #endregion
    } // enum MsgBoxExButton
} // namespace
