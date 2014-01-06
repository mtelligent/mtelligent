using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mtelligent.Entities;
using Mtelligent.Entities.Cohorts;
using Mtelligent.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mtelligent.Web.Tests
{
    [TestClass]
    public class ExperimentManagerTests
    {
        public void SimpleTestGetHypothesis()
        {
            Experiment exp = new Experiment()
            {
                Id=100,
                Name = "TestExp",
                SystemName = "TestExp",
                TargetCohort = new AllUsersCohort()
                {
                     Id=200,
                     Name="AllUsers",
                     SystemName="AllUsers"
                },
                Segments = new List<ExperimentSegment>()
            };

            var segment1 = new ExperimentSegment()
            {
                 Id=300,
                 ExperimentId=100,
                 TargetPercentage=50,
                 Name="Option1",
                 SystemName="Option1"
            };

            exp.Segments.Add(segment1);

            var segment2 = new ExperimentSegment()
            {
                 Id=301,
                 ExperimentId=100,
                 TargetPercentage=50,
                 Name="Option2",
                 SystemName="Option2"
            };

            exp.Segments.Add(segment2);

            var visitor = new Visitor()
            {
                ExperimentSegmentsLoaded=true,
                CohortsLoaded=true,
                AttributesLoaded=true,
                ConverstionsLoaded=true,
                DetailsLoaded=true,
                LandingUrlsLoaded=true,
                ReferrersLoaded=true,
                ExperimentSegments= new List<ExperimentSegment>(),
                Cohorts= new List<Cohort>(),
                Attributes=new Dictionary<string,string>(),
                Conversions=new List<Goal>(),
                FirstVisit=DateTime.Now             
            };

            var hyp = ExperimentManager.Current.GetHypothesis(exp, visitor);

            Assert.IsNotNull(hyp);
            Assert.IsTrue(hyp == segment1 || hyp == segment2);
            Assert.AreEqual<int>(1, visitor.ExperimentSegments.Count);
            Assert.AreEqual<int>(1, visitor.Request.ExperimentSegments.Count);
        }

        [TestMethod]
        public void TestGetDefault()
        {
            Experiment exp = new Experiment()
            {
                Id = 100,
                Name = "TestExp",
                SystemName = "TestExp",
                TargetCohort = new AuthenticatedUsersCohort()
                {
                    Id = 200,
                    Name = "AllUsers",
                    SystemName = "AllUsers"
                },
                Segments = new List<ExperimentSegment>()
            };

            var segment1 = new ExperimentSegment()
            {
                Id = 300,
                ExperimentId = 100,
                TargetPercentage = 50,
                Name = "Option1",
                SystemName = "Option1"
            };

            exp.Segments.Add(segment1);

            var segment2 = new ExperimentSegment()
            {
                Id = 301,
                ExperimentId = 100,
                TargetPercentage = 50,
                Name = "Option2",
                SystemName = "Option2"
            };

            exp.Segments.Add(segment2);

            var defSeg = new ExperimentSegment()
            {
                Id = 302,
                ExperimentId = 100,
                TargetPercentage = 0,
                Name = "Default",
                SystemName = "Default",
                IsDefault=true
            };

            exp.Segments.Add(defSeg);

            var visitor = new Visitor()
            {
                ExperimentSegmentsLoaded = true,
                CohortsLoaded = true,
                AttributesLoaded = true,
                ConverstionsLoaded = true,
                DetailsLoaded = true,
                LandingUrlsLoaded = true,
                ReferrersLoaded = true,
                ExperimentSegments = new List<ExperimentSegment>(),
                Cohorts = new List<Cohort>(),
                Attributes = new Dictionary<string, string>(),
                Conversions = new List<Goal>(),
                FirstVisit = DateTime.Now,
                IsAuthenticated=false,
            };

            var hyp = ExperimentManager.Current.GetHypothesis(exp, visitor);

            Assert.IsNotNull(hyp);
            Assert.IsTrue(hyp == defSeg);
            Assert.AreEqual<int>(0, visitor.ExperimentSegments.Count);
            Assert.AreEqual<int>(0, visitor.Request.ExperimentSegments.Count);
        }
    }
}
