using System;
using MuonLab.Web.Xhtml.Components.Implementations;
using MuonLab.Web.Xhtml.Tests.Components.FormattableComponentSpecifications;

namespace MuonLab.Web.Xhtml.Tests.Components.TextBoxSpecifications
{
    public class FormattedAsSpecification : FormattedAsStringSpecification<TextBoxComponent<TestEntity, DateTime>, DateTime>
    {
        protected override DateTime value
        {
            get { return new DateTime(2009, 12, 12); }
        }

        protected override string formatString
        {
            get { return "dd/MM/yyyy"; }
        }

        protected override string expectedRendering
        {
            get { return "<input type=\"text\" value=\"12/12/2009\" />"; }
        }
    }
}