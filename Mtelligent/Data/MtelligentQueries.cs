using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mtelligent.Data
{
    public class MtelligentQueries
    {
        public const string GetExperiment = @"
            Select @CohortId=TargetCohortId, @ExperimentId=Id from Experiments (nolock) where SystemName = @SystemName
            Select * from Experiments (nolock) where Id = @ExperimentId
            Select * from ExperimentSegments (nolock) where ExperimentId = @ExperimentId
            Select ExperimentSegmentId, Name, Value from ExperimentVariables (nolock) A 
                inner join ExperimentSegmentVariableValues (nolock) B on A.Id = B.ExperimentVariableId 
                Where A.ExperimentID = @ExperimentId
            Select * from Cohorts (nolock) Where Id = @CohortId
            Select * from CohortProperties (nolock) where CohortId = @CohortId";

        public const string GetGoal = @"Select * from Goals (nolock) where SystemName = @GoalName";

        public const string AddVisitor = @"INSERT INTO[Visitors] ([UID],[FirstVisit],[UserName],[IsAuthenticated]) VALUES
                                           (@UID, @FirstVisit, @UserName, @IsAuthenticated) 
                                          Select * from Visitors where Id = scope_Identity()";

        public const string UpdateVisitor = "Update Visitors set UserName=@UserName, IsAuthenticated=@IsAuthenticated Where UID = @UID";

        public const string GetVisitor = @"Select * from Visitors (nolock) where UID = @UID";
        public const string GetVisitorFromUserName = @"Select * from Visitors (nolock) where UserName = @UserName";

        public const string ReconcileVisitor = @"Select @ExistingID=ID from Visitors (nolock) where UserName=@UserName
                                                 if (@ExistingID is not null)
                                                 Begin
                                                     Select @VisitorID=ID from Visitors (nolock) where UID=@UID
                                                     Update Visitors Set ReconcilledVisitorId = @ExistingID where ID=@VisitorID
                                                     Update VisitorReferrers set VisitorId=@ExistingID where VisitorId = @VisitorID
                                                     Update VisitorLandingPages set VisitorId=@ExistingID where VisitorId = @VisitorID
                                                     Update VisitorRequests set VisitorId=@ExistingID where VisitorId = @VisitorID
                                                     Update VisitorAttributes set VisitorId=@ExistingID where VisitorId = @VisitorID and Name not in
                                                         (Select Name from VisitorAttributes where VisitorId=@ExistingID)
                                                     Update VisitorCohorts set VisitorId=@ExistingID where VisitorId = @VisitorID and CohortId not in
                                                         (Select CohortId from VisitorCohorts where VisitorId=@ExistingID)
                                                     Update VisitorConversions set VisitorId=@ExistingID where VisitorId = @VisitorID and GoalId not in
                                                         (Select GoalId from VisitorConversions where VisitorId=@ExistingID)
                                                     Update VisitorSegments set VisitorId=@ExistingID where VisitorId = @VisitorID and ExperimentId not in
                                                         (Select ExperimentId from VisitorSegments where VisitorId=@ExistingID)
                                                 End
                                                 Select * from Visitors (nolock) where Id = @ExistingID or (@ExistingID is null and UID=@UID)";

        public const string GetVisitorAttributes = @"Select A.* from VisitorAttributes (nolock) A 
                                                     inner join Visitors (nolock) B on A.VisitorId = B.Id where B.UID = @UID";

        public const string GetVisitorLandingPages = @"Select A.* from VisitorLandingPages (nolock) A 
                                                       inner join Visitors (nolock) B on A.VisitorId = B.Id where B.UID = @UID";

        public const string GetVisitorReferrers = @"Select A.* from VisitorReferrers (nolock) A 
                                                    inner join Visitors (nolock) B on A.VisitorId = B.Id where B.UID = @UID";

        public const string GetVisitorCohorts = @"Select C.* from VisitorCohorts (nolock) A 
                                                  inner join Visitors (nolock) B on A.VisitorId = B.Id 
                                                  inner join Cohorts (nolock) C on A.CohortID = C.Id where B.UID = @UID
                                                  
                                                  Select C.* from VisitorCohorts (nolock) A 
                                                  inner join Visitors (nolock) B on A.VisitorId = B.Id 
                                                  inner join CohortProperties (nolock) C on C.CohortId = A.CohortId where B.UID = @UID";

        public const string GetVisitorConversions = @"Select C.* from VisitorConversions (nolock) A 
                                                      inner join Visitors (nolock) B on A.VisitorId = B.Id 
                                                      inner join Goals (nolock) C on A.GoalId = C.Id where B.UID = @UID";

        public const string GetVisitorSegments = @"Select C.* from VisitorSegments (nolock) A 
                                                   inner join Visitors (nolock) B on A.VisitorId = B.Id 
                                                   inner join ExperimentSegments (nolock) C on A.SegmentId = C.Id where B.UID = @UID

                                                    Select ExperimentSegmentId, Name, Value from VisitorSegments (nolock) A
                                                    inner join Visitors (nolock) B on A.VisitorId = B.Id 
                                                    inner join ExperimentVariables (nolock) C on A.ExperimentId = c.ExperimentId
                                                    inner join ExperimentSegmentVariableValues (nolock) D on A.SegmentId = D.ExperimentSegmentId and C.Id = ExperimentVariableId
                                                    Where B.UID = @UID";


        public const string AddVisitorAttribute = @"If not exists (Select 1 from VisitorAttributes (nolock) where VisitorId=@VisitorId and Name=@Name)
            Insert into VisitorAttributes (VisitorId, Name, Value) Values (@VisitorId, @Name, @Value)";

        public const string RemoveVisitorAttribute = @"Delete from VisitorAttributes where VisitorId=@VisitorId and Name=@Name";

        public const string AddVisitorCohort = @"If not exists (Select 1 from VisitorCohorts (nolock) where VisitorId=@VisitorId and CohortId=@CohortId)
           Insert into VisitorCohorts (VisitorId, CohortId) Values (@VisitorId, @CohortId)";
        
        public const string AddVisitorConversion = @"Insert into VisitorConversions (VisitorId, GoalId) Values (@VisitorId, @GoalId)";

        public const string AddVisitorLandingPage = @"If not exists (Select 1 from VisitorLandingPages (nolock) where VisitorId=@VisitorId and LandingPageUrl=@LandingPageUrl)
            Insert into VisitorLandingPages (VisitorId, LandingPageUrl) values (@VisitorId, @LandingPageUrl)";

        public const string AddVisitorReferrer = @"If not exists (Select 1 from VisitorReferrers (nolock) where VisitorId=@VisitorId and ReferrerUrl=@ReferrerUrl)
            Insert into VisitorReferrers (VisitorId, ReferrerUrl) values (@VisitorId, @ReferrerUrl)";

        public const string AddVisitorRequest = @"Insert into VisitorRequests (VisitorId, RequestUrl) values (@VisitorId, @RequestUrl)";

        public const string AddVisitorSegment = @"If not exists (Select 1 from VisitorSegments (nolock) where VisitorId=@VisitorId and SegmentId=@SegmentId)
            Insert into VisitorSegments (VisitorId, SegmentId, ExperimentId) values (@VisitorId, @SegmentId, @ExperimentId)";

        public const string GetCohort = @"Select * from Cohorts where SystemName = @SystemName";
    }
}
