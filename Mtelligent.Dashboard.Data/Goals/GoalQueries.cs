using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mtelligent.Dashboard.Data
{
    public class GoalQueries
    {
        public const string GetGoals = @"Select * from Goals where Active=1";
        public const string GetGoal = @"Select * from Goals where Id = @GoalId";
        public const string AddGoal = 
                @"Insert into Goals (Name, SystemName, GACode, CustomJS, Created, CreatedBy) Values (@Name, @SystemName, @GACode, @CustomJS, getDate(), @CreatedBy);
                select * from Goals where Id = scope_Identity()";
        public const string UpdateGoal = @"Update Goals Set Name=@Name, GACode=@GACode, CustomJS=@CustomJS, Updated=getDate(), UpdatedBy=@UpdatedBy Where Id = @GoalId";
        public const string DeleteGoal = @"Update Goals set Active=0, Updated=getDate(), UpdatedBy=@UpdatedBy where Id = @GoalId";

    }
}
