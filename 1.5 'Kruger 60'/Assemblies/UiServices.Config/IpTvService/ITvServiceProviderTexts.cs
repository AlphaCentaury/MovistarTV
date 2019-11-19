namespace IpTviewr.UiServices.Configuration.IpTvService
{
    public interface ITvServiceProviderTexts
    {
        // ChannelListForm.resx: menuItemDvbProvider.Text
        // &Provider
        // &Proveedor
        string MenuEntry { get; }

        // ChannelListForm.resx: menuItemProviderSelect.Text
        // Select provider...
        // &Seleccionar proveedor...
        string MenuSelect { get; }

        // ChannelListForm.resx: menuItemProviderDetails.Text
        // Show technical details...
        // Ver &detalles técnicos...
        string MenuDetails { get; }

        // * ChannelList.Texts:NotSelectedServiceProvider
        // No service provider has been selected
        // No se ha seleccionado un proveedor de servicios
        string NoSelection { get; }

        // * ChannelList.Texts: SPListUnableRefresh
        // UiServices.Forms.DiscoveryTexts: SPListUnableRefresh
        // Unable to obtain the list of service providers
        // No ha sido posible obtener la lista de proveedores de servicios
        string ListRefreshError { get; }

        // * ChannelList.Texts: SPObtainingList
        // UiServices.Forms.DiscoveryTexts: SPObtainingList
        // Obtaining the list of service providers...
        // Obteniendo la lista de proveedores de servicios...
        string ObtainingList { get; }

        // * ChannelList.Texts: SPParsingList
        // UiServices.Forms.DiscoveryTexts: SPParsingList
        // Parsing and extracting the list of providers...
        // Analizando y extrayendo la lista de proveedores...
        string ParsingList { get; }

        // * ChannelList.Texts: SPProperties
        // UiServices.Forms.DiscoveryTexts: SPProperties
        // Properties of the service provider
        // Propiedades del proveedor de servicios
        string PropertiesCaption { get; }

        // UiServices.Config.Texts: ExceptionLogosProviderImageLoadError
        // Unable to load provider logo file\r\n{0}
        // Imposible cargar el archivo con el logo del proveedor\r\n{0}
        string LogoLoadError { get; }

        // UiServices.Config.Texts: ExceptionLogosProviderImageNotFound
        // Provider logo file not found
        // El archivo con el logo del proveedor no existe
        string LogoNotFound { get; }

        // UiServices.Discovery.Texts: ProviderUnknownDisplayDescription
        // No description is available for this service provider
        // No existe una descripción de este proveedor de servicios
        string UnknownDisplayDescription { get; }

        // UiServices.Discovery.Texts: FormatProviderFriendlyDisplayName
        string FormatFriendlyName { get; }

        // UiServices.Discovery.Texts: FormatProviderUnknownDisplayName
        string FormatUnknownName { get; }

        // SelectProviderDialog.resx: $this.Text
        // Select service provider
        // Selección de proveedor de servicios
        string SelectCaption { get; }
    } // interface ITvServiceProviderTexts
} // namespace