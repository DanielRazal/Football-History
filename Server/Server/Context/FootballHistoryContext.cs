﻿namespace Server.Context
{
    public class ServerContext : DbContext
    {
        public ServerContext() { }

        public ServerContext(DbContextOptions<ServerContext> options) : base(options) { }

        public virtual DbSet<Player> Players { get; set; } = null!;
        public virtual DbSet<Club> Clubs { get; set; } = null!;
        public virtual DbSet<ClubPlayer> ClubPlayer { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Answer> Answers { get; set; } = null!;
        public virtual DbSet<Question> Questions { get; set; } = null!;
        public virtual DbSet<Quiz> Quizzes { get; set; } = null!;
        public virtual DbSet<CompletedQuiz> CompletedQuizzes { get; set; } = null!;
        public virtual DbSet<SelectedAnswer> SelectedAnswers { get; set; } = null!;
        public virtual DbSet<Message> Messages { get; set; } = null!;
        public virtual DbSet<Position> Positions { get; set; } = null!;
        public virtual DbSet<Nationality> Nationalities { get; set; } = null!;

        private int CalculatePlayerScore(int goals, int assists, int goalContributions, int titles, int appearances, double goalsRatio, double assistsRatio)
        {
            int score = (int)(goals * 2 + assists * 1 + appearances * 0.5
                + titles * 10 + goalContributions * 2.5 +
                goalsRatio * 150 + assistsRatio * 100);

            return score;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Position>()
            .HasData(
                new
                {
                    Id = 1,
                    PositionName = PositionSelection.RW
                },
                new
                {
                    Id = 2,
                    PositionName = PositionSelection.ST
                },
                new
                {
                    Id = 3,
                    PositionName = PositionSelection.CF
                },
                new
                {
                    Id = 4,
                    PositionName = PositionSelection.LW
                }
            );


            modelBuilder.Entity<Nationality>()
            .HasData(
                new
                {
                    Id = 1,
                    NationalityName = NationalitySelection.Argentina
                },
                new
                {
                    Id = 2,
                    NationalityName = NationalitySelection.Portugal
                },
                new
                {
                    Id = 3,
                    NationalityName = NationalitySelection.Brazil
                },
                new
                {
                    Id = 4,
                    NationalityName = NationalitySelection.Netherlands
                },
                new
                {
                    Id = 5,
                    NationalityName = NationalitySelection.Hungary
                }
            );

            modelBuilder.Entity<Player>(entity =>
            {
                entity.HasData(
                new
                {
                    Id = 1,
                    Photo = "https://www.aljazeera.com/wp-content/uploads/2022/12/SSS10772_1.jpg?resize=770%2C513&quality=80",
                    FullName = "Lionel Messi",
                    Goals = 807,
                    Assists = 357,
                    GoalContributions = 1164,
                    Titles = 43,
                    Appearances = 1028,
                    DateOfBirth = new DateTime(1987, 6, 24).ToString("yyyy-MM-dd"),
                    GoalsRatio = 0.79,
                    AssistsRatio = 0.34,
                    Information = "Lionel Andrés Messi (born 24 June 1987) is an Argentine footballer. He plays for Inter Miami and the Argentina national team as a striker.Every expert and critic consider Messi as one of the greatest football players of all time. Some people even say he is the greatest player ever.Messi has seven Ballon d'Or awards, the most of any player, and two more than Legend Cristiano Ronaldo.His playing style and skills are very similar to the Argentine legend Diego Maradona because of their short height.There is much competition between him and Portuguese footballer Cristiano Ronaldo because of their similar skill levels.",
                    NationalityId = 1,
                    PositionId = 1,
                    Score = CalculatePlayerScore(807, 357, 1164, 43, 1028, 0.79, 0.34)
                }
                ,
                new
                {
                    Id = 2,
                    Photo = "https://cdn.images.express.co.uk/img/dynamic/67/750x445/1444303.jpg",
                    FullName = "Cristiano Ronaldo",
                    Goals = 838,
                    Assists = 236,
                    GoalContributions = 1074,
                    Titles = 32,
                    Appearances = 1168,
                    DateOfBirth = new DateTime(1985, 2, 05).ToString("yyyy-MM-dd"),
                    GoalsRatio = 0.71,
                    AssistsRatio = 0.2,
                    Information =
                    "Cristiano Ronaldo dos Santos Aveiro born 5 February 1985) is a Portuguese professional footballer who plays as a forward for and captains both Saudi Professional League club Al Nassr and the Portugal national team.Widely regarded as one of the greatest players of all time, Ronaldo has won five Ballon d'Or awards and four European Golden Shoes, the most by a European player. He has won 32 trophies in his career, including seven league titles,five UEFA Champions Leagues, the UEFA European Championship and the UEFA Nations League. Ronaldo holds the records for most appearances (183),goals (140) and assists (42) in the Champions League, goals in the European Championship (14), international goals (123) and international appearances (200).He is one of the few players to have made over 1,100 professional career appearances, and has scored over 800 official senior career goals for club and country,making him the highest goalscorer of all time. He is the only player to score in five different FIFA World Cup tournaments.",
                    NationalityId = 2,
                    PositionId = 2,
                    Score = CalculatePlayerScore(838, 236, 1074, 32, 1168, 0.71, 0.2)
                },
                new
                {
                    Id = 3,
                    Photo = "https://ca-times.brightspotcdn.com/dims4/default/27fed8d/2147483647/strip/true/crop/2625x1697+0+0/resize/1200x776!/quality/80/?url=https%3A%2F%2Fcalifornia-times-brightspot.s3.amazonaws.com%2F4a%2F6b%2F5326bd3645d6a92ce8c6a7cc4b15%2Fwcup-six-pack-greatest-players-soccer.JPEG",
                    FullName = "Pele",
                    Goals = 756,
                    Assists = 367,
                    GoalContributions = 1123,
                    Titles = 26,
                    Appearances = 1363,
                    DateOfBirth = new DateTime(1940, 10, 23).ToString("yyyy-MM-dd"),
                    GoalsRatio = 0.55,
                    AssistsRatio = 0.26,
                    Information =
                    "Edson Arantes do Nascimento born  23 October 1940, better known by his nickname Pelé , was a Brazilian professional footballer who played as a forward. Widely regarded as one of the greatest players of all time, he was among the most successful and popular sports figures of the 20th century. In 1999, he was named Athlete of the Century by the International Olympic Committee and was included in the Time list of the 100 most important people of the 20th century. In 2000, Pelé was voted World Player of the Century by the International Federation of Football History & Statistics (IFFHS) and was one of the two joint winners of the FIFA Player of the Century. His 756 goals in 1,363 games, which includes friendlies, is recognised as a Guinness World Record.",
                    NationalityId = 3,
                    PositionId = 2,
                    Score = CalculatePlayerScore(756, 367, 1123, 26, 1363, 0.55, 0.26)
                },
                new
                {
                    Id = 4,
                    Photo = "https://i.guim.co.uk/img/media/44af12b1a6150abab1da744f8aefc5081d86153a/0_266_4860_2917/master/4860.jpg?width=1200&height=1200&quality=85&auto=format&fit=crop&s=8a4de25d6e0026705571fbedc6580c75",
                    FullName = "Diego Maradona",
                    Goals = 346,
                    Assists = 213,
                    GoalContributions = 559,
                    Titles = 11,
                    Appearances = 490,
                    DateOfBirth = new DateTime(1960, 10, 30).ToString("yyyy-MM-dd"),
                    GoalsRatio = 0.70,
                    AssistsRatio = 0.43,
                    Information =
                    "Diego Armando Maradona (born 30 October 1960 – 25 November 2020) was an Argentine professional football player and manager. Widely regarded as one of the greatest players in the history of the sport, he was one of the two joint winners of the FIFA Player of the 20th Century award. Maradona achieved an iconic moment in his career by leading Argentina to victory in the FIFA World Cup in 1986.",
                    NationalityId = 1,
                    PositionId = 3,
                    Score = CalculatePlayerScore(346, 213, 559, 11, 490, 0.70, 0.43)
                },
                new
                {
                    Id = 5,
                    Photo = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSkZbV4y1R4rhqKE_vxixUXfX9Ke-hFAGJH5A&usqp=CAU",
                    FullName = "Johan Cruyff",
                    Goals = 403,
                    Assists = 358,
                    GoalContributions = 761,
                    Titles = 18,
                    Appearances = 720,
                    DateOfBirth = new DateTime(1947, 4, 25).ToString("yyyy-MM-dd"),
                    GoalsRatio = 0.55,
                    AssistsRatio = 0.49,
                    Information =
                    "Johan Cruyff (born 25 April 1947 – 24 March 2016) was a Dutch professional football player and manager. Regarded as one of the greatest players of all time, and as the greatest Dutch footballer ever, he won the Ballon d'Or three times, in 1971, 1973, and 1974, while playing for Barcelona. Because of the far-reaching impact of his playing style and his coaching ideas, he is widely regarded as one of the most influential figures in modern football. For his achievements as a manager, he is also viewed as one of the greatest managers of all time.",
                    NationalityId = 4,
                    PositionId = 3,
                    Score = CalculatePlayerScore(403, 358, 761, 18, 720, 0.55, 0.49)
                },
                new
                {
                    Id = 6,
                    Photo = "https://www.lippiehippie.com/wp-content/uploads/2022/12/R9-haircut.jpg",
                    FullName = "Ronaldo Nazário",
                    Goals = 342,
                    Assists = 59,
                    GoalContributions = 401,
                    Titles = 18,
                    Appearances = 482,
                    DateOfBirth = new DateTime(1976, 9, 18).ToString("yyyy-MM-dd"),
                    GoalsRatio = 0.70,
                    AssistsRatio = 0.12,
                    Information =
                    "Ronaldo Luís Nazário de Lima born 18 September 1976), known as Ronaldo or Ronaldo Nazário, is a Brazilian business owner and president of Segunda Division club Real Valladolid, owner of Brasileiro Série A club Cruzeiro, and a former professional footballer who played as a striker. Nicknamed  Fenômeno ('The Phenomenon') and R9, he is considered as one of the greatest players in the history of the sport. As a multi-functional striker who brought a new dimension to the position, Ronaldo has been an influence for a generation of strikers that have followed. His individual accolades include being named FIFA World Player of the Year three times and winning two Ballon d'Or awards.",
                    NationalityId = 3,
                    PositionId = 2,
                    Score = CalculatePlayerScore(342, 59, 401, 18, 482, 0.70, 0.12)
                },
                new
                {
                    Id = 7,
                    Photo = "https://football3105blog.files.wordpress.com/2017/11/ronaldino-afp-big_.jpg",
                    FullName = "Ronaldinho Gaúcho",
                    Goals = 238,
                    Assists = 165,
                    GoalContributions = 403,
                    Titles = 10,
                    Appearances = 608,
                    DateOfBirth = new DateTime(1980, 3, 21).ToString("yyyy-MM-dd"),
                    GoalsRatio = 0.39,
                    AssistsRatio = 0.27,
                    Information =
                    "Ronaldo de Assis Moreira (born 21 March 1980), commonly known as Ronaldinho Gaúcho  or simply Ronaldinho, is a Brazilian retired professional footballer who played mostly as an attacking midfielder, but was also deployed as a winger. Widely regarded as one of the greatest players of all time, he won two FIFA World Player of the Year awards and a Ballon d'Or. He is the only player ever to have won a World Cup, a Copa América, a Confederations Cup, a Champions League, a Copa Libertadores and a Ballon d'Or. A global icon of the sport, Ronaldinho was renowned for his technical skills, creativity, dribbling ability and accuracy from free-kicks, his use of tricks, feints, no-look passes and overhead kicks, as well as his ability to score and create goals.",
                    NationalityId = 3,
                    PositionId = 4,
                    Score = CalculatePlayerScore(238, 165, 403, 10, 608, 0.39, 0.27)
                },
                new
                {
                    Id = 8,
                    Photo = "https://i.eurosport.com/2011/02/26/694410-21498655-2560-1440.jpg",
                    FullName = "Di Stéfano",
                    Goals = 377,
                    Assists = 282,
                    GoalContributions = 659,
                    Titles = 19,
                    Appearances = 733,
                    DateOfBirth = new DateTime(1926, 7, 4).ToString("yyyy-MM-dd"),
                    GoalsRatio = 0.51,
                    AssistsRatio = 0.38,
                    Information =
                    "Alfredo Stéfano Di Stéfano Laulhé born 4 July 1926 – 7 July 2014) was a professional footballer and coach who played as a forward, regarded as one of the greatest footballers of all time. he is best known for his achievements with Real Madrid, where he was instrumental in the club's domination of the European Cup and La Liga during the 1950s and 1960s. Along with Francisco Gento and José María Zárraga, he was one of only three players to play a part in all five European Cup victories, scoring goals in each of the five finals.",
                    NationalityId = 1,
                    PositionId = 2,
                    Score = CalculatePlayerScore(377, 282, 659, 19, 733, 0.51, 0.38)
                },
                new
                {
                    Id = 9,
                    Photo = "https://pbs.twimg.com/media/Ex4SxuBWgAAJwH7.jpg",
                    FullName = "Ferenc Puskás",
                    Goals = 746,
                    Assists = 404,
                    GoalContributions = 1150,
                    Titles = 22,
                    Appearances = 793,
                    DateOfBirth = new DateTime(1927, 4, 17).ToString("yyyy-MM-dd"),
                    GoalsRatio = 0.94,
                    AssistsRatio = 0.50,
                    Information =
                    "Ferenc Puskás born Ferenc Purczeld; April 1927 – 17 was a Hungarian footballer and manager, widely regarded as one of the greatest players of all time and the sport's first international superstar. A forward and an attacking midfielder, he scored 84 goals in 85 international matches for Hungary and played four international matches for Spain. He became an Olympic champion in 1952 and led his nation to the final of the 1954 World Cup. He won three European Cups (1959, 1960, 1966), ten national championships (five Hungarian and five Spanish Primera División) and eight top individual scoring honors. In 1995, he was recognized as the greatest top division scorer of the 20th century by the IFFHS. With 746 goals in 793 official games scored during his career, he is the seventh top goalscorer of all time.",
                    NationalityId = 5,
                    PositionId = 3,
                    Score = CalculatePlayerScore(746, 404, 1150, 22, 793, 0.94, 0.50)
                },
                new
                {
                    Id = 10,
                    Photo = "https://tmssl.akamaized.net/images/foto/galerie/eusebio-1670414809-98149.jpg?lm=1670414822",
                    FullName = "Eusébio",
                    Goals = 733,
                    Assists = 79,
                    GoalContributions = 812,
                    Titles = 17,
                    Appearances = 766,
                    DateOfBirth = new DateTime(1942, 1, 25).ToString("yyyy-MM-dd"),
                    GoalsRatio = 0.95,
                    AssistsRatio = 0.10,
                    Information =
                    "Eusébio da Silva Ferreira born 25 January 1942, nicknamed the \"Black Panther\", was a Portuguese footballer who played as a striker. He is considered one of the greatest players of all time as well as Benfica's best player ever. He was known for his speed, technique, athleticism and his ferocious right-footed shot, making him a prolific goalscorer, accumulating 733 goals in 766 matches.",
                    NationalityId = 2,
                    PositionId = 3,
                    Score = CalculatePlayerScore(733, 79, 812, 17, 766, 0.95, 0.10)
                }
                );
            });

            modelBuilder.Entity<Club>()
            .HasData(
                new Club { Id = 1, ClubName = ClubSelection.Barcelona },
                new Club { Id = 2, ClubName = ClubSelection.PSG },
                new Club { Id = 3, ClubName = ClubSelection.Inter_Miami },
                new Club { Id = 4, ClubName = ClubSelection.Sporting_Lisbon },
                new Club { Id = 5, ClubName = ClubSelection.Manchester_United },
                new Club { Id = 6, ClubName = ClubSelection.Real_Madrid },
                new Club { Id = 7, ClubName = ClubSelection.Juventus },
                new Club { Id = 8, ClubName = ClubSelection.Al_Nassr },
                new Club { Id = 9, ClubName = ClubSelection.Santos },
                new Club { Id = 10, ClubName = ClubSelection.New_York_Cosmos },
                new Club { Id = 11, ClubName = ClubSelection.Argentinos_Juniors },
                new Club { Id = 12, ClubName = ClubSelection.Boca_Juniors },
                new Club { Id = 13, ClubName = ClubSelection.Napoli },
                new Club { Id = 14, ClubName = ClubSelection.Sevilla },
                new Club { Id = 15, ClubName = ClubSelection.Newells_Old_Boys },
                new Club { Id = 16, ClubName = ClubSelection.Ajax },
                new Club { Id = 17, ClubName = ClubSelection.Barcelona_ },
                new Club { Id = 18, ClubName = ClubSelection.Los_Angeles_Aztecs },
                new Club { Id = 19, ClubName = ClubSelection.Washington_Diplomats },
                new Club { Id = 20, ClubName = ClubSelection.Levante },
                new Club { Id = 21, ClubName = ClubSelection.Feyenoord },
                new Club { Id = 22, ClubName = ClubSelection.Cruzeiro },
                new Club { Id = 23, ClubName = ClubSelection.PSV },
                new Club { Id = 24, ClubName = ClubSelection.Barcelona__ },
                new Club { Id = 25, ClubName = ClubSelection.Inter_Milan },
                new Club { Id = 26, ClubName = ClubSelection.Real__Madrid },
                new Club { Id = 27, ClubName = ClubSelection.AC_Milan },
                new Club { Id = 28, ClubName = ClubSelection.Corinthians },
                new Club { Id = 29, ClubName = ClubSelection.Grêmio },
                new Club { Id = 30, ClubName = ClubSelection.PSG_ },
                new Club { Id = 31, ClubName = ClubSelection.Barcelona___ },
                new Club { Id = 32, ClubName = ClubSelection.AC_Milan_ },
                new Club { Id = 33, ClubName = ClubSelection.Flamengo },
                new Club { Id = 34, ClubName = ClubSelection.Atlético_Mineiro },
                new Club { Id = 35, ClubName = ClubSelection.Querétaro },
                new Club { Id = 36, ClubName = ClubSelection.Fluminense },
                new Club { Id = 37, ClubName = ClubSelection.River_Plate },
                new Club { Id = 38, ClubName = ClubSelection.Millonarios },
                new Club { Id = 39, ClubName = ClubSelection.Real___Madrid },
                new Club { Id = 40, ClubName = ClubSelection.Espanyol },
                new Club { Id = 41, ClubName = ClubSelection.Budapest_Honvéd },
                new Club { Id = 42, ClubName = ClubSelection.Real____Madrid },
                new Club { Id = 43, ClubName = ClubSelection.Sporting_Lourenço_Marques },
                new Club { Id = 44, ClubName = ClubSelection.Benfica },
                new Club { Id = 45, ClubName = ClubSelection.Boston_Minutemen },
                new Club { Id = 46, ClubName = ClubSelection.Monterrey },
                new Club { Id = 47, ClubName = ClubSelection.Toronto_Metros_Croatia },
                new Club { Id = 48, ClubName = ClubSelection.Beira_Mar },
                new Club { Id = 49, ClubName = ClubSelection.Las_Vegas_Quicksilvers },
                new Club { Id = 50, ClubName = ClubSelection.União_de_Tomar },
                new Club { Id = 51, ClubName = ClubSelection.New_Jersey_Americans },
                new Club { Id = 52, ClubName = ClubSelection.Buffalo_Stalions }
            );

            modelBuilder.Entity<ClubPlayer>()
            .HasData(
                new ClubPlayer { Id = 1, ClubId = 1, PlayerId = 1 },
                new ClubPlayer { Id = 2, ClubId = 2, PlayerId = 1 },
                new ClubPlayer { Id = 3, ClubId = 3, PlayerId = 1 },
                new ClubPlayer { Id = 4, ClubId = 4, PlayerId = 2 },
                new ClubPlayer { Id = 5, ClubId = 5, PlayerId = 2 },
                new ClubPlayer { Id = 6, ClubId = 6, PlayerId = 2 },
                new ClubPlayer { Id = 7, ClubId = 7, PlayerId = 2 },
                new ClubPlayer { Id = 8, ClubId = 8, PlayerId = 2 },
                new ClubPlayer { Id = 9, ClubId = 9, PlayerId = 3 },
                new ClubPlayer { Id = 10, ClubId = 10, PlayerId = 3 },
                new ClubPlayer { Id = 11, ClubId = 11, PlayerId = 4 },
                new ClubPlayer { Id = 12, ClubId = 12, PlayerId = 4 },
                new ClubPlayer { Id = 13, ClubId = 1, PlayerId = 4 },
                new ClubPlayer { Id = 14, ClubId = 13, PlayerId = 4 },
                new ClubPlayer { Id = 15, ClubId = 14, PlayerId = 4 },
                new ClubPlayer { Id = 16, ClubId = 15, PlayerId = 4 },
                new ClubPlayer { Id = 17, ClubId = 16, PlayerId = 5 },
                new ClubPlayer { Id = 18, ClubId = 17, PlayerId = 5 },
                new ClubPlayer { Id = 19, ClubId = 18, PlayerId = 5 },
                new ClubPlayer { Id = 20, ClubId = 19, PlayerId = 5 },
                new ClubPlayer { Id = 21, ClubId = 20, PlayerId = 5 },
                new ClubPlayer { Id = 22, ClubId = 21, PlayerId = 5 },
                new ClubPlayer { Id = 23, ClubId = 22, PlayerId = 6 },
                new ClubPlayer { Id = 24, ClubId = 23, PlayerId = 6 },
                new ClubPlayer { Id = 25, ClubId = 24, PlayerId = 6 },
                new ClubPlayer { Id = 26, ClubId = 25, PlayerId = 6 },
                new ClubPlayer { Id = 27, ClubId = 26, PlayerId = 6 },
                new ClubPlayer { Id = 28, ClubId = 27, PlayerId = 6 },
                new ClubPlayer { Id = 29, ClubId = 28, PlayerId = 6 },
                new ClubPlayer { Id = 30, ClubId = 29, PlayerId = 7 },
                new ClubPlayer { Id = 31, ClubId = 30, PlayerId = 7 },
                new ClubPlayer { Id = 32, ClubId = 31, PlayerId = 7 },
                new ClubPlayer { Id = 33, ClubId = 32, PlayerId = 7 },
                new ClubPlayer { Id = 34, ClubId = 33, PlayerId = 7 },
                new ClubPlayer { Id = 35, ClubId = 34, PlayerId = 7 },
                new ClubPlayer { Id = 36, ClubId = 35, PlayerId = 7 },
                new ClubPlayer { Id = 37, ClubId = 36, PlayerId = 7 },
                new ClubPlayer { Id = 38, ClubId = 37, PlayerId = 8 },
                new ClubPlayer { Id = 39, ClubId = 38, PlayerId = 8 },
                new ClubPlayer { Id = 40, ClubId = 39, PlayerId = 8 },
                new ClubPlayer { Id = 41, ClubId = 40, PlayerId = 8 },
                new ClubPlayer { Id = 42, ClubId = 41, PlayerId = 9 },
                new ClubPlayer { Id = 43, ClubId = 42, PlayerId = 9 },
                new ClubPlayer { Id = 44, ClubId = 43, PlayerId = 10 },
                new ClubPlayer { Id = 45, ClubId = 44, PlayerId = 10 },
                new ClubPlayer { Id = 46, ClubId = 45, PlayerId = 10 },
                new ClubPlayer { Id = 47, ClubId = 46, PlayerId = 10 },
                new ClubPlayer { Id = 48, ClubId = 47, PlayerId = 10 },
                new ClubPlayer { Id = 49, ClubId = 48, PlayerId = 10 },
                new ClubPlayer { Id = 50, ClubId = 49, PlayerId = 10 },
                new ClubPlayer { Id = 51, ClubId = 50, PlayerId = 10 },
                new ClubPlayer { Id = 52, ClubId = 51, PlayerId = 10 },
                new ClubPlayer { Id = 53, ClubId = 52, PlayerId = 10 }
            );

            modelBuilder.Entity<User>()
            .HasData(
                new
                {
                    Id = 1,
                    FirstName = "Daniel",
                    LastName = "Razal",
                    UserName = "DanielR129",
                    Password = "DanielR1!",
                    Email = "mr.danielrazal@gmail.com",
                    PhotoUrl = "https://media.licdn.com/dms/image/C4D03AQG6xMCLMbZt0g/profile-displayphoto-shrink_800_800/0/1653056557647?e=2147483647&v=beta&t=utzmimR5C-pAK5rGYUuS3pTCADz6qaN9NFYlKVVD3FY"
                },
                new
                {
                    Id = 2,
                    FirstName = "Lior",
                    LastName = "David",
                    UserName = "LiorD129",
                    Password = "LiorD123!",
                    Email = "mr.liordavid@gmail.com",
                    PhotoUrl = "https://images.unsplash.com/photo-1499996860823-5214fcc65f8f?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Nnx8cmFuZG9tJTIwcGVyc29ufGVufDB8fDB8fHww&w=1000&q=80"
                },
                new
                {
                    Id = 3,
                    FirstName = "Opal",
                    LastName = "Yuri",
                    UserName = "OpalY129",
                    Password = "OpalYuri1!",
                    Email = "mr.opalyuri@gmail.com",
                    PhotoUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTkOLTJCZB1n8jj5_6usj63FKX3MkRuU9Jtnw&usqp=CAU"
                },
                new User
                {
                    Id = 4,
                    FirstName = "Tomer",
                    LastName = "Amir",
                    UserName = "TomerA129",
                    Password = "TomerAmir1!",
                    Email = "mr.tomeramir@gmail.com",
                    PhotoUrl = "https://www.beaconjournal.com/gcdn/presto/2021/05/12/NABJ/6f48888a-98f3-46cb-ae5b-ad8d06c6b8c5-IMG_Mark_J_Price_Photo_1_1_.JPG?width=660&height=908&fit=crop&format=pjpg&auto=webp"
                }
            );

            modelBuilder.Entity<Quiz>()
            .HasData(
                new Quiz
                {
                    Id = 1,
                    Name = "Football Quiz"
                }
            );


            modelBuilder.Entity<QuestionType>()
            .HasData(
                new QuestionType
                {
                    Id = 1,
                    Type = QuestionTypeSelection.SelectOne
                },
                new QuestionType
                {
                    Id = 2,
                    Type = QuestionTypeSelection.SelectMany
                }
            );

            modelBuilder.Entity<Question>()
                .HasData(
                    new Question
                    {
                        Id = 1,
                        Title = "What is Lionel Messi's current club?",
                        QuestionTypeId = 1,
                        QuizId = 1
                    }
                    ,
                    new Question
                    {
                        Id = 2,
                        Title = "How many goals has Lionel Messi scored in his career?",
                        QuestionTypeId = 1,
                        QuizId = 1
                    }
                    ,
                    new Question
                    {
                        Id = 3,
                        Title = "In which club did Cristiano Ronaldo NOT play during his career?",
                        QuestionTypeId = 1,
                        QuizId = 1
                    },
                    new Question
                    {
                        Id = 4,
                        Title = "How many Ballon d'Or awards has Cristiano Ronaldo won?",
                        QuestionTypeId = 1,
                        QuizId = 1
                    },
                    new Question
                    {
                        Id = 5,
                        Title = "How many titles did Pelé win throughout his career?",
                        QuestionTypeId = 1,
                        QuizId = 1
                    },
                    new Question
                    {
                        Id = 6,
                        Title = "What is Pelé's nationality?",
                        QuestionTypeId = 1,
                        QuizId = 1
                    },
                    new Question
                    {
                        Id = 7,
                        Title = "When was Diego Maradona born?",
                        QuestionTypeId = 1,
                        QuizId = 1
                    },
                    new Question
                    {
                        Id = 8,
                        Title = "In what year did Diego Maradona win the FIFA World Cup?",
                        QuestionTypeId = 1,
                        QuizId = 1
                    },
                    new Question
                    {
                        Id = 9,
                        Title = "In which club did Johan Cruyff win the Ballon d'Or three times?",
                        QuestionTypeId = 1,
                        QuizId = 1
                    },
                    new Question
                    {
                        Id = 10,
                        Title = "For which national team did Johan Cruyff play during his international career?",
                        QuestionTypeId = 1,
                        QuizId = 1
                    },
                    new Question
                    {
                        Id = 11,
                        Title = "Which club did Ronaldo Nazário join after his tenure at Barcelona?",
                        QuestionTypeId = 1,
                        QuizId = 1
                    },
                    new Question
                    {
                        Id = 12,
                        Title = "How many appearances did Ronaldo Nazário make in his professional career?",
                        QuestionTypeId = 1,
                        QuizId = 1
                    },
                    new Question
                    {
                        Id = 13,
                        Title = "How many FIFA World Player of the Year awards did Ronaldinho Gaúcho win?",
                        QuestionTypeId = 1,
                        QuizId = 1
                    },
                    new Question
                    {
                        Id = 14,
                        Title = "What position did Ronaldinho Gaúcho primarily play during his career?",
                        QuestionTypeId = 1,
                        QuizId = 1
                    },
                    new Question
                    {
                        Id = 15,
                        Title = "How many European Cup victories did Alfredo Di Stéfano play a part in?",
                        QuestionTypeId = 1,
                        QuizId = 1
                    }
                    ,
                    new Question
                    {
                        Id = 16,
                        Title = "Where did Alfredo Di Stéfano dominate in the 1950s and 1960s?",
                        QuestionTypeId = 1,
                        QuizId = 1
                    },
                    new Question
                    {
                        Id = 17,
                        Title = "When did Ferenc Puskás become an Olympic champion?",
                        QuestionTypeId = 1,
                        QuizId = 1
                    },
                    new Question
                    {
                        Id = 18,
                        Title = "How many top individual scoring honors did Ferenc Puskás win during his career?",
                        QuestionTypeId = 1,
                        QuizId = 1
                    },
                    new Question
                    {
                        Id = 19,
                        Title = "Eusébio, a legendary Portuguese footballer, was famously known by which nickname?",
                        QuestionTypeId = 1,
                        QuizId = 1
                    }
                    ,
                    new Question
                    {
                        Id = 20,
                        Title = "Eusébio da Silva Ferreira, the renowned footballer, hailed from which country?",
                        QuestionTypeId = 1,
                        QuizId = 1
                    }

                    ,
                    new Question
                    {
                        Id = 21,
                        Title = "Which players won the Ballon d'Or??",
                        QuestionTypeId = 2,
                        QuizId = 1
                    }

                    ,
                    new Question
                    {
                        Id = 22,
                        Title = "Which players were born in Argentina?",
                        QuestionTypeId = 2,
                        QuizId = 1
                    }

                    ,
                    new Question
                    {
                        Id = 23,
                        Title = "Which players played in Barcelona or Real Madrid?",
                        QuestionTypeId = 2,
                        QuizId = 1
                    }

                    ,
                    new Question
                    {
                        Id = 24,
                        Title = "Which players played in the striker position?",
                        QuestionTypeId = 2,
                        QuizId = 1
                    }

                    ,
                    new Question
                    {
                        Id = 25,
                        Title = "Which teams won the Champions League?",
                        QuestionTypeId = 2,
                        QuizId = 1
                    }
                );

            modelBuilder.Entity<Answer>()
                .HasData(
                    new Answer
                    {
                        Id = 1,
                        Text = "Barcelona",
                        IsCorrect = false,
                        QuestionId = 1
                    }
                    ,
                    new Answer
                    {
                        Id = 2,
                        Text = "PSG",
                        IsCorrect = false,
                        QuestionId = 1
                    },
                    new Answer
                    {
                        Id = 3,
                        Text = "Inter Miami",
                        IsCorrect = true,
                        QuestionId = 1
                    },
                    new Answer
                    {
                        Id = 4,
                        Text = "None of the above",
                        IsCorrect = false,
                        QuestionId = 1
                    }

                ,

                new Answer
                {
                    Id = 5,
                    Text = "603",
                    IsCorrect = false,
                    QuestionId = 2
                },
                new Answer
                {
                    Id = 6,
                    Text = "718",
                    IsCorrect = false,
                    QuestionId = 2
                },
                new Answer
                {
                    Id = 7,
                    Text = "938",
                    IsCorrect = false,
                    QuestionId = 2
                },
                new Answer
                {
                    Id = 8,
                    Text = "807",
                    IsCorrect = true,
                    QuestionId = 2
                }


                ,

                new Answer
                {
                    Id = 9,
                    Text = "Real Madrid",
                    IsCorrect = false,
                    QuestionId = 3
                },
                new Answer
                {
                    Id = 10,
                    Text = "Al Nassr",
                    IsCorrect = false,
                    QuestionId = 3
                },
                new Answer
                {
                    Id = 11,
                    Text = "Manchester United",
                    IsCorrect = false,
                    QuestionId = 3
                },
                new Answer
                {
                    Id = 12,
                    Text = "Manchester City",
                    IsCorrect = true,
                    QuestionId = 3
                },

                new Answer
                {
                    Id = 13,
                    Text = "3",
                    IsCorrect = false,
                    QuestionId = 4
                },
                new Answer
                {
                    Id = 14,
                    Text = "4",
                    IsCorrect = false,
                    QuestionId = 4
                },
                new Answer
                {
                    Id = 15,
                    Text = "5",
                    IsCorrect = true,
                    QuestionId = 4
                },
                new Answer
                {
                    Id = 16,
                    Text = "6",
                    IsCorrect = false,
                    QuestionId = 4
                },

                new Answer
                {
                    Id = 17,
                    Text = "20",
                    IsCorrect = false,
                    QuestionId = 5
                },
                new Answer
                {
                    Id = 18,
                    Text = "26",
                    IsCorrect = true,
                    QuestionId = 5
                },
                new Answer
                {
                    Id = 19,
                    Text = "32",
                    IsCorrect = false,
                    QuestionId = 5
                },
                new Answer
                {
                    Id = 20,
                    Text = "40",
                    IsCorrect = false,
                    QuestionId = 5
                },

                new Answer
                {
                    Id = 21,
                    Text = "Argentina",
                    IsCorrect = false,
                    QuestionId = 6
                },
                new Answer
                {
                    Id = 22,
                    Text = "Brazil",
                    IsCorrect = true,
                    QuestionId = 6
                },
                new Answer
                {
                    Id = 23,
                    Text = "Portugal",
                    IsCorrect = false,
                    QuestionId = 6
                },
                new Answer
                {
                    Id = 24,
                    Text = "France",
                    IsCorrect = false,
                    QuestionId = 6
                },

                new Answer
                {
                    Id = 25,
                    Text = "1960-10-30",
                    IsCorrect = true,
                    QuestionId = 7
                },
                new Answer
                {
                    Id = 26,
                    Text = "1960-10-31",
                    IsCorrect = false,
                    QuestionId = 7
                },
                new Answer
                {
                    Id = 27,
                    Text = "1960-10-29",
                    IsCorrect = false,
                    QuestionId = 7
                },
                new Answer
                {
                    Id = 28,
                    Text = "1960-10-28",
                    IsCorrect = false,
                    QuestionId = 7
                },

                new Answer
                {
                    Id = 29,
                    Text = "1982",
                    IsCorrect = false,
                    QuestionId = 8
                },
                new Answer
                {
                    Id = 30,
                    Text = "1986",
                    IsCorrect = true,
                    QuestionId = 8
                },
                new Answer
                {
                    Id = 31,
                    Text = "1990",
                    IsCorrect = false,
                    QuestionId = 8
                },
                new Answer
                {
                    Id = 32,
                    Text = "1994",
                    IsCorrect = false,
                    QuestionId = 8
                },

                new Answer
                {
                    Id = 33,
                    Text = "Feyenoord",
                    IsCorrect = false,
                    QuestionId = 9
                },
                new Answer
                {
                    Id = 34,
                    Text = "Los Angeles Aztecs",
                    IsCorrect = false,
                    QuestionId = 9
                },
                new Answer
                {
                    Id = 35,
                    Text = "Ajax",
                    IsCorrect = false,
                    QuestionId = 9
                },
                new Answer
                {
                    Id = 36,
                    Text = "Barcelona",
                    IsCorrect = true,
                    QuestionId = 9
                },

                new Answer
                {
                    Id = 37,
                    Text = "Netherlands",
                    IsCorrect = true,
                    QuestionId = 10
                },
                new Answer
                {
                    Id = 38,
                    Text = "Spain",
                    IsCorrect = false,
                    QuestionId = 10
                },
                new Answer
                {
                    Id = 39,
                    Text = "Germany",
                    IsCorrect = false,
                    QuestionId = 10
                },
                new Answer
                {
                    Id = 40,
                    Text = "Belgium",
                    IsCorrect = false,
                    QuestionId = 10
                },

                new Answer
                {
                    Id = 41,
                    Text = "Cruzeiro",
                    IsCorrect = false,
                    QuestionId = 11
                },
                new Answer
                {
                    Id = 42,
                    Text = "Real Madrid",
                    IsCorrect = false,
                    QuestionId = 11
                },
                new Answer
                {
                    Id = 43,
                    Text = "AC Milan",
                    IsCorrect = false,
                    QuestionId = 11
                },
                new Answer
                {
                    Id = 44,
                    Text = "Inter Milan",
                    IsCorrect = true,
                    QuestionId = 11
                },

                new Answer
                {
                    Id = 45,
                    Text = "342 appearances",
                    IsCorrect = false,
                    QuestionId = 12
                },
                new Answer
                {
                    Id = 46,
                    Text = "401 appearances",
                    IsCorrect = false,
                    QuestionId = 12
                },
                new Answer
                {
                    Id = 47,
                    Text = "482 appearances",
                    IsCorrect = true,
                    QuestionId = 12
                },
                new Answer
                {
                    Id = 48,
                    Text = "372 appearances",
                    IsCorrect = false,
                    QuestionId = 12
                },

                new Answer
                {
                    Id = 49,
                    Text = "0",
                    IsCorrect = false,
                    QuestionId = 13
                },
                new Answer
                {
                    Id = 50,
                    Text = "1",
                    IsCorrect = false,
                    QuestionId = 13
                },
                new Answer
                {
                    Id = 51,
                    Text = "2",
                    IsCorrect = true,
                    QuestionId = 13
                },
                new Answer
                {
                    Id = 52,
                    Text = "3",
                    IsCorrect = false,
                    QuestionId = 13
                },

                new Answer
                {
                    Id = 53,
                    Text = "ST",
                    IsCorrect = false,
                    QuestionId = 14
                },
                new Answer
                {
                    Id = 54,
                    Text = "LW",
                    IsCorrect = true,
                    QuestionId = 14
                },
                new Answer
                {
                    Id = 55,
                    Text = "CAM",
                    IsCorrect = false,
                    QuestionId = 14
                },
                new Answer
                {
                    Id = 56,
                    Text = "RW",
                    IsCorrect = false,
                    QuestionId = 14
                },

                new Answer
                {
                    Id = 57,
                    Text = "2",
                    IsCorrect = false,
                    QuestionId = 15
                },
                new Answer
                {
                    Id = 58,
                    Text = "3",
                    IsCorrect = false,
                    QuestionId = 15
                },
                new Answer
                {
                    Id = 59,
                    Text = "4",
                    IsCorrect = false,
                    QuestionId = 15
                },
                new Answer
                {
                    Id = 60,
                    Text = "5",
                    IsCorrect = true,
                    QuestionId = 15
                },

                new Answer
                {
                    Id = 61,
                    Text = "Real Madrid",
                    IsCorrect = true,
                    QuestionId = 16
                },
                new Answer
                {
                    Id = 62,
                    Text = "Barcelona",
                    IsCorrect = false,
                    QuestionId = 16
                },
                new Answer
                {
                    Id = 63,
                    Text = "Millonarios",
                    IsCorrect = false,
                    QuestionId = 16
                },
                new Answer
                {
                    Id = 64,
                    Text = "River Plate",
                    IsCorrect = false,
                    QuestionId = 16
                },

                new Answer
                {
                    Id = 65,
                    Text = "1948",
                    IsCorrect = false,
                    QuestionId = 17
                },
                new Answer
                {
                    Id = 66,
                    Text = "1952",
                    IsCorrect = true,
                    QuestionId = 17
                },
                new Answer
                {
                    Id = 67,
                    Text = "1956",
                    IsCorrect = false,
                    QuestionId = 17
                },
                new Answer
                {
                    Id = 68,
                    Text = "1960",
                    IsCorrect = false,
                    QuestionId = 17
                },

                new Answer
                {
                    Id = 69,
                    Text = "4",
                    IsCorrect = false,
                    QuestionId = 18
                },
                new Answer
                {
                    Id = 70,
                    Text = "6",
                    IsCorrect = false,
                    QuestionId = 18
                },
                new Answer
                {
                    Id = 71,
                    Text = "7",
                    IsCorrect = false,
                    QuestionId = 18
                },
                new Answer
                {
                    Id = 72,
                    Text = "8",
                    IsCorrect = true,
                    QuestionId = 18
                },

                new Answer
                {
                    Id = 73,
                    Text = "The Lion",
                    IsCorrect = false,
                    QuestionId = 19
                },
                new Answer
                {
                    Id = 74,
                    Text = "The Magician",
                    IsCorrect = false,
                    QuestionId = 19
                },
                new Answer
                {
                    Id = 75,
                    Text = "The Black Panther",
                    IsCorrect = true,
                    QuestionId = 19
                },
                new Answer
                {
                    Id = 76,
                    Text = "The Golden Boot",
                    IsCorrect = false,
                    QuestionId = 19
                },

                new Answer
                {
                    Id = 77,
                    Text = "Brazil",
                    IsCorrect = false,
                    QuestionId = 20
                },
                new Answer
                {
                    Id = 78,
                    Text = "Spain",
                    IsCorrect = false,
                    QuestionId = 20
                },
                new Answer
                {
                    Id = 79,
                    Text = "Portugal",
                    IsCorrect = true,
                    QuestionId = 20
                },
                new Answer
                {
                    Id = 80,
                    Text = "Italy",
                    IsCorrect = false,
                    QuestionId = 20
                }

                ,

                new Answer
                {
                    Id = 81,
                    Text = "Johan Cruyff",
                    IsCorrect = true,
                    QuestionId = 21
                },
                new Answer
                {
                    Id = 82,
                    Text = "Pele",
                    IsCorrect = false,
                    QuestionId = 21
                },
                new Answer
                {
                    Id = 83,
                    Text = "Lionel Messi",
                    IsCorrect = true,
                    QuestionId = 21
                },
                new Answer
                {
                    Id = 84,
                    Text = "Diego Maradona",
                    IsCorrect = false,
                    QuestionId = 21
                }

                ,

                new Answer
                {
                    Id = 85,
                    Text = "Ferenc Puskás",
                    IsCorrect = false,
                    QuestionId = 22
                },
                new Answer
                {
                    Id = 86,
                    Text = "Di Stéfano",
                    IsCorrect = true,
                    QuestionId = 22
                },
                new Answer
                {
                    Id = 87,
                    Text = "Lionel Messi",
                    IsCorrect = true,
                    QuestionId = 22
                },
                new Answer
                {
                    Id = 88,
                    Text = "Diego Maradona",
                    IsCorrect = true,
                    QuestionId = 22
                }

                ,

                new Answer
                {
                    Id = 89,
                    Text = "Ronaldo Nazário",
                    IsCorrect = true,
                    QuestionId = 23
                },
                new Answer
                {
                    Id = 90,
                    Text = "Eusébio",
                    IsCorrect = false,
                    QuestionId = 23
                },
                new Answer
                {
                    Id = 91,
                    Text = "Ronaldinho Gaúcho",
                    IsCorrect = true,
                    QuestionId = 23
                },
                new Answer
                {
                    Id = 92,
                    Text = "Pele",
                    IsCorrect = false,
                    QuestionId = 23
                }

                ,

                new Answer
                {
                    Id = 93,
                    Text = "Ronaldo Nazário",
                    IsCorrect = true,
                    QuestionId = 24
                },
                new Answer
                {
                    Id = 94,
                    Text = "Cristiano Ronaldo",
                    IsCorrect = true,
                    QuestionId = 24
                },
                new Answer
                {
                    Id = 95,
                    Text = "Lionel Messi",
                    IsCorrect = false,
                    QuestionId = 24
                },
                new Answer
                {
                    Id = 96,
                    Text = "Diego Maradona",
                    IsCorrect = false,
                    QuestionId = 24
                }

                ,

                new Answer
                {
                    Id = 97,
                    Text = "PSG",
                    IsCorrect = false,
                    QuestionId = 25
                },
                new Answer
                {
                    Id = 98,
                    Text = "Barcelona",
                    IsCorrect = true,
                    QuestionId = 25
                },
                new Answer
                {
                    Id = 99,
                    Text = "Benfica",
                    IsCorrect = true,
                    QuestionId = 25
                },
                new Answer
                {
                    Id = 100,
                    Text = "Napoli",
                    IsCorrect = false,
                    QuestionId = 25
                }
            );
        }
    }

}