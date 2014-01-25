Select * from Cohorts where Type = 'Mtelligent.Entities.Cohorts.AllUsersCohort, Mtelligent';

if (@@ROWCOUNT = 0)
begin

Insert into Cohorts (Name, SystemName, Type, Created, CreatedBy) Values ('All Users', 'All users', 'Mtelligent.Entities.Cohorts.AllUsersCohort, Mtelligent', getDate(), 'System')

end

Select * from Cohorts where Type = 'Mtelligent.Entities.Cohorts.AuthenticatedUsersCohort, Mtelligent';

if (@@ROWCOUNT = 0)
begin

Insert into Cohorts (Name, SystemName, Type, Created, CreatedBy) Values ('Authenticated Users', 'Authenticated users', 'Mtelligent.Entities.Cohorts.AuthenticatedUsersCohort, Mtelligent', getDate(), 'System')

end


Select * from Cohorts where Type = 'Mtelligent.Entities.Cohorts.NonAuthenticatedUsersCohort, Mtelligent';

if (@@ROWCOUNT = 0)
begin

Insert into Cohorts (Name, SystemName, Type, Created, CreatedBy) Values ('Non Authenticated Users', 'Non Authenticated users', 'Mtelligent.Entities.Cohorts.NonAuthenticatedUsersCohort, Mtelligent', getDate(), 'System')

end
GO
