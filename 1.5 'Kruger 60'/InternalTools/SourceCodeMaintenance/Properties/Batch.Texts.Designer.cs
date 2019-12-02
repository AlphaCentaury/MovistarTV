// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Batch_Texts {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Batch_Texts() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("AlphaCentaury.Tools.SourceCodeMaintenance.Properties.Batch.Texts", typeof(Batch_Texts).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Arguments are not optional.
        /// </summary>
        internal static string ArgumentsNotOptional {
            get {
                return ResourceManager.GetString("ArgumentsNotOptional", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot add tool.
        /// </summary>
        internal static string ArgumentsNotOptionalCaption {
            get {
                return ResourceManager.GetString("ArgumentsNotOptionalCaption", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Yes = Save changes and create a new batch
        ///No = Discard changes and create a new batch
        ///Cancel = Keep current batch.
        /// </summary>
        internal static string NewBatchSaveExplanation {
            get {
                return ResourceManager.GetString("NewBatchSaveExplanation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Yes = Save changes and open a new batch
        ///No = Discard changes and open a new batch
        ///Cancel = Keep current batch and don&apos;t open a new batch.
        /// </summary>
        internal static string OpenBatchSaveExplanation {
            get {
                return ResourceManager.GetString("OpenBatchSaveExplanation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Save changes to current batch?.
        /// </summary>
        internal static string SaveBatchChanges {
            get {
                return ResourceManager.GetString("SaveBatchChanges", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Save changes.
        /// </summary>
        internal static string SaveBatchChangesCaption {
            get {
                return ResourceManager.GetString("SaveBatchChangesCaption", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0}
        ///
        ///{1}.
        /// </summary>
        internal static string SaveIfDirty {
            get {
                return ResourceManager.GetString("SaveIfDirty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Batch files|*.batch.
        /// </summary>
        internal static string SelectFileFilter {
            get {
                return ResourceManager.GetString("SelectFileFilter", resourceCulture);
            }
        }
    }
}