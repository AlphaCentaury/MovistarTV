namespace AlphaCentaury.Licensing.Data.Serialization
{
    public class LicensedInstaller: LicensedItem
    {
        public string Technology { get; set; }

        public override LicensedItemType Type => LicensedItemType.Installer;

        protected override LicensedItem CreateNewForCloning()
        {
            var result = new LicensedInstaller();
            result.Technology = Technology;

            return result;
        } // CreateNewForCloning
    } // class LicensedInstaller
} // namespace
