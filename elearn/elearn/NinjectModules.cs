using Ninject.Modules;
using elearn.JournalService;
using elearn.ProfileService;
using elearn.CourseService;
using elearn.GroupService;
using elearn.TestService;
using elearn.LearningMaterialService;

namespace elearn
{
    public class ProfileModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IProfileService>().To<ProfileServiceClient>().InSingletonScope();
            Bind<ICourseService>().To<CourseServiceClient>().InSingletonScope();
            Bind<IGroupService>().To<GroupServiceClient>().InSingletonScope();
            Bind<ITestService>().To<TestServiceClient>().InSingletonScope();
            Bind<IJournalService>().To<JournalServiceClient>().InSingletonScope();
            Bind<ILearningMaterialService>().To<LearningMaterialServiceClient>().InSingletonScope();

        }
    }
}