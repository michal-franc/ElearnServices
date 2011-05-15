using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Modules;
using elearn.ProfileService;
using elearn.CourseService;
using elearn.GroupService;

namespace elearn
{
    public class ProfileModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IProfileService>().To<ProfileServiceClient>().InRequestScope();
            Bind<ICourseService>().To<CourseServiceClient>().InRequestScope();
            Bind<IGroupService>().To<GroupServiceClient>().InRequestScope();
        }
    }
}