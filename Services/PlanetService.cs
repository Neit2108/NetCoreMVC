using NetCoreMVC.Models;

namespace NetCoreMVC.Services;

public class PlanetService : List<PlanetModel>{

    public PlanetService(){
        Add(new PlanetModel{
            ID = 1,
            Name = "Mercury",
            Vietnamese_name = "Sao Thủy",
            Content = "Mercury is the smallest planet in the Solar System and the closest to the Sun. Its orbit around the Sun takes 87.97 Earth days, the shortest of all the planets in the Solar System."
        });
        // add planet
        Add(new PlanetModel{
            ID = 2,
            Name = "Venus",
            Vietnamese_name = "Sao Kim",
            Content = "Venus is the second planet from the Sun. It is named after the Roman goddess of love and beauty. As the brightest natural object in Earth's night sky after the Moon, Venus can cast shadows and can be, on rare occasion, visible to the naked eye in broad daylight."
        });
        // add planet
        Add(new PlanetModel{
            ID = 3,
            Name = "Earth",
            Vietnamese_name = "Trái Đất",
            Content = "Earth is the third planet from the Sun and the only astronomical object known to harbor and support life. About 29.2% of Earth's surface is land consisting of continents and islands."
        });
        // add planet
        Add(new PlanetModel{
            ID = 4,
            Name = "Mars",
            Vietnamese_name = "Sao Hỏa",
            Content = "Mars is the fourth planet from the Sun and the second-smallest planet in the Solar System, being larger than only Mercury. In English, Mars carries the name of the Roman god of war and is often referred to as the 'Red Planet'."
        });
        // add planet
        Add(new PlanetModel{
            ID = 5,
            Name = "Jupiter",
            Vietnamese_name = "Sao Mộc",
            Content = "Jupiter is the fifth planet from the Sun and the largest in the Solar System. It is a gas giant with a mass more than two and a half times that of all the other planets in the Solar System combined, but slightly less than one-thousandth the mass of the Sun."
        });
        // add planet
        Add(new PlanetModel{
            ID = 6,
            Name = "Saturn",
            Vietnamese_name = "Sao Thổ",
            Content = "Saturn is the sixth planet from the Sun and the second-largest in the Solar System, after Jupiter. It is a gas giant with an average radius of about nine times that of Earth."
        });
        // add planet
        Add(new PlanetModel{
            ID = 7,
            Name = "Uranus",
            Vietnamese_name = "Sao Thiên Vương",
            Content = "Uranus is the seventh planet from the Sun. Its name is a reference to the Greek god of the sky, Uranus, who, according to Greek mythology, was the grandfather of Zeus and father of Cronus."
        });
        // add planet
        Add(new PlanetModel{
            ID = 8,
            Name = "Neptune",
            Vietnamese_name = "Sao Hải Vương",
            Content = "Neptune is the eighth and farthest-known Solar planet from the Sun. In the Solar System, it is the fourth-largest planet by diameter, the third-most-massive planet, and the densest giant planet."
        });
    }
}