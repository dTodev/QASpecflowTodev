namespace Todev.SpecflowProject.Tests.StepsDefinitions
{
    [Binding]
    public class BogusSteps
    {
        [Given(@"I want to create new user requst body")]
        public void GivenIWantToCreateNewUserRequstBody()
        {
            //var fakerUser = new Faker<GoRESTRequestUser>()
            //    .RuleFor(u => u.Gender, f => f.PickRandom<Bogus.DataSets.Name.Gender>())
            //    .RuleFor(u => u.FirstName, (f, u) => f.Name.FirstName(u.Gender))
            //    .RuleFor(u => u.LastName, (f, u) => f.Name.LastName(u.Gender))
            //    .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
            //    .RuleFor(u => u.Status, f => f.PickRandom<Status>().ToString());

            //var requestBody = fakerUser.Generate();
        }
    }
}
