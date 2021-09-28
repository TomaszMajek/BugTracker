﻿using BugTracker.DataAccess;
using BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.BusinessLogic
{
    public class ProjectProcessor
    {
        public static int CreateProject(string projectName, string description)
        {
            ProjectModel data = new ProjectModel
            {
                ProjectName = projectName,
                Description = description,
            };

            string sql = @"insert into dbo.Projects (ProjectName, Description)
                            values (@ProjectName, @Description);";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static List<ProjectModel> LoadTickets()
        {
            string sql = @"select ProjectId, ProjectName, Description from dbo.Projects;";

            return SqlDataAccess.LoadData<ProjectModel>(sql);
        }
    }
}