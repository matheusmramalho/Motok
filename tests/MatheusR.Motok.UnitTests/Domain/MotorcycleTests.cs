using MatheusR.Motok.Domain.Entities;

namespace MatheusR.Motok.Domain.Tests.Entities
{
    public class MotorcycleTests
    {
        [Fact]
        public void Constructor_WithValidParameters_ShouldCreateInstance()
        {
            // Arrange
            var identifier = "MOT-001";
            var year = 2022;
            var model = "Honda CB 500";
            var licencePlate = "XYZ-1234";

            // Act
            var motorcycle = new Motorcycle(identifier, year, model, licencePlate);

            // Assert
            Assert.NotNull(motorcycle);
            Assert.NotEqual(Guid.Empty, motorcycle.Id);
            Assert.Equal(identifier, motorcycle.Identifier);
            Assert.Equal(year, motorcycle.Year);
            Assert.Equal(model, motorcycle.Model);
            Assert.Equal(licencePlate, motorcycle.LicencePlate);
            Assert.NotNull(motorcycle.Rents);
            Assert.Empty(motorcycle.Rents);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Constructor_WithInvalidIdentifier_ShouldThrowArgumentException(string invalidIdentifier)
        {
            // Arrange
            var year = 2022;
            var model = "Honda CB 500";
            var licencePlate = "XYZ-1234";

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(
                () => new Motorcycle(invalidIdentifier, year, model, licencePlate));

            Assert.Equal("Identifier cannot be null or empty.", exception.Message);
        }

        [Theory]
        [InlineData(1884)]
        [InlineData(0)]
        [InlineData(-100)]
        public void Constructor_WithInvalidYear_ShouldThrowArgumentException(int invalidYear)
        {
            // Arrange
            var identifier = "MOT-001";
            var model = "Honda CB 500";
            var licencePlate = "XYZ-1234";

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(
                () => new Motorcycle(identifier, invalidYear, model, licencePlate));

            Assert.Equal("Invalid motorcycle year.", exception.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Constructor_WithInvalidModel_ShouldThrowArgumentException(string invalidModel)
        {
            // Arrange
            var identifier = "MOT-001";
            var year = 2022;
            var licencePlate = "XYZ-1234";

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(
                () => new Motorcycle(identifier, year, invalidModel, licencePlate));

            Assert.Equal("Model cannot be null or empty.", exception.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Constructor_WithInvalidLicencePlate_ShouldThrowArgumentException(string invalidLicencePlate)
        {
            // Arrange
            var identifier = "MOT-001";
            var year = 2022;
            var model = "Honda CB 500";

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(
                () => new Motorcycle(identifier, year, model, invalidLicencePlate));

            Assert.Equal("LicencePlate cannot be null or empty.", exception.Message);
        }

        [Fact]
        public void Constructor_ShouldGenerateUniqueIds()
        {
            // Arrange
            var identifier = "MOT-001";
            var year = 2022;
            var model = "Honda CB 500";
            var licencePlate = "XYZ-1234";

            // Act
            var motorcycle1 = new Motorcycle(identifier, year, model, licencePlate);
            var motorcycle2 = new Motorcycle(identifier, year, model, licencePlate);

            // Assert
            Assert.NotEqual(motorcycle1.Id, motorcycle2.Id);
        }

        [Fact]
        public void Constructor_WithMinimumValidYear_ShouldCreateInstance()
        {
            // Arrange
            var identifier = "MOT-001";
            var year = 1886; // First motorcycle was invented in 1885, so 1886 is valid
            var model = "Honda CB 500";
            var licencePlate = "XYZ-1234";

            // Act
            var motorcycle = new Motorcycle(identifier, year, model, licencePlate);

            // Assert
            Assert.Equal(year, motorcycle.Year);
        }

        [Fact]
        public void ModifyLicencePlate_WithValidPlate_ShouldUpdateValue()
        {
            // Arrange
            var motorcycle = new Motorcycle("MOT-001", 2022, "Honda CB 500", "OLD-1234");
            var newPlate = "NEW-5678";

            // Act
            motorcycle.ModifyLicencePlate(newPlate);

            // Assert
            Assert.Equal(newPlate, motorcycle.LicencePlate);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void ModifyLicencePlate_WithInvalidPlate_ShouldThrowArgumentException(string invalidPlate)
        {
            // Arrange
            var motorcycle = new Motorcycle("MOT-001", 2022, "Honda CB 500", "OLD-1234");

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(
                () => motorcycle.ModifyLicencePlate(invalidPlate));

            Assert.Equal("LicencePlate cannot be null or empty.", exception.Message);
        }
    }
}