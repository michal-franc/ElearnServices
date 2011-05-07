﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Modules;
using elearn.ProfileService;
using elearn.CourseService;

namespace elearn
{
    public class ProfileModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IProfileService>().To<ProfileServiceClient>().InRequestScope();
            Bind<ICourseService>().To<CourseServiceClient>().InRequestScope();
        }
    }
}