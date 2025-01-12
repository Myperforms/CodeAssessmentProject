using CodeAssessment.Test.Common;
using Xunit;

namespace CodeAssessment.Test
{
    public abstract class TestBase : IClassFixture<ServiceLocationSetup>
    {
        protected readonly ServiceLocationSetup serviceLocationSetup;
        protected ApplicationService Services => serviceLocationSetup.Services;
        public TestBase(ServiceLocationSetup serviceLocationSetup)
        {
            this.serviceLocationSetup = serviceLocationSetup;
        }
    }
}
