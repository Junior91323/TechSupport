namespace TechSupport.DAL.Migrations
{
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TechSupport.DAL.EF.TechSupportContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TechSupport.DAL.EF.TechSupportContext context)
        {
            //context.EmployeeState.AddRange(new List<EmployeeState> {
            //    new EmployeeState { Title= "busy"},
            //    new EmployeeState { Title= "free"},

            //});
            //context.RequestState.AddRange(new List<RequestState> {
            //    new RequestState { Title= "processing"},
            //    new RequestState { Title= "new"},
            //    new RequestState { Title= "completed"},
            //    new RequestState { Title = "canceled" }
            //});

            //Position _operator = new Position() { Title = "Operator", WaitingTimeSec = 10 };
            //Position _manager = new Position() { Title = "Manager", WaitingTimeSec = 20 };
            //Position _director = new Position() { Title = "Director", WaitingTimeSec = 30 };

            //List<Position> positions = new List<Position>();
            //positions.Add(_operator);
            //positions.Add(_manager);
            //positions.Add(_director);

            //context.Positions.AddRange(positions);

            //context.Employees.AddRange(new List<Employee> {
            //    new Employee { Name="Operator_1", LastName="Operator_LN_1", PositionId = 1},
            //    new Employee { Name="Operator_2", LastName="Operator_LN_2", PositionId = 1 },
            //    new Employee { Name="Operator_3", LastName="Operator_LN_3", PositionId = 1},
            //    new Employee { Name="Operator_4", LastName="Operator_LN_4", PositionId = 1 },
            //    new Employee { Name="Operator_5", LastName="Operator_LN_5", PositionId = 1 },
            //    new Employee { Name="Operator_6", LastName="Operator_LN_6", PositionId = 1 },
            //    new Employee { Name="Manager_1", LastName="Manager_LN_1", PositionId = 2 },
            //    new Employee { Name="Manager_2", LastName="Manager_LN_2", PositionId = 2 },
            //    new Employee { Name="Director_1", LastName="Director_LN_1", PositionId = 3 }
            //});

        }
    }
}
