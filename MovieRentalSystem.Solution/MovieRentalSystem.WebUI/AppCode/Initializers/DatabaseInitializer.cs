using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;
using MovieRentalSystem.WebUI.Models.Entities.Membership;
using MovieRentalSystem.WebUI.Models.Entities.Membership.Credentials;

namespace MovieRentalSystem.WebUI.AppCode.Initializers
{
    public static class DatabaseInitializer
    {
        async public static Task<IApplicationBuilder> InjectData(this IApplicationBuilder app)
        {
            using (IServiceScope scope = app.ApplicationServices.CreateScope())
            {
                MovieDbContext db = scope.ServiceProvider.GetRequiredService<MovieDbContext>();
                RoleManager<AppRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
                UserManager<AppUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

                db.Database.Migrate();

                AppRole roleResult = await roleManager.FindByNameAsync("Admin");

                //---Identity---
                if (roleResult == null)
                {
                    roleResult = new AppRole
                    {
                        Name = "Admin"
                    };

                    IdentityResult roleResponse = await roleManager.CreateAsync(roleResult);

                    if (roleResponse.Succeeded)
                    {
                        AppUser userResult = await userManager.FindByNameAsync("rahimlizakir");

                        if (userResult == null)
                        {
                            userResult = new AppUser
                            {
                                UserName = "rahimlizakir",
                                Email = "zakirer@code.edu.az"
                            };

                            IdentityResult userResponse = await userManager.CreateAsync(userResult, AdminCredential.Pick());

                            if (userResponse.Succeeded)
                            {
                                IdentityResult roleUserResult = await userManager.AddToRoleAsync(userResult, roleResult.Name);
                            }
                        }
                        else
                        {
                            IdentityResult roleUserResult = await userManager.AddToRoleAsync(userResult, roleResult.Name);
                        }
                    }
                }
                else
                {
                    AppUser userResult = await userManager.FindByNameAsync("rahimlizakir");

                    if (userResult == null)
                    {
                        userResult = new AppUser
                        {
                            UserName = "rahimlizakir",
                            Email = "zakirer@code.edu.az"
                        };

                        IdentityResult userResponse = await userManager.CreateAsync(userResult, AdminCredential.Pick());

                        if (userResponse.Succeeded)
                        {
                            IdentityResult roleUserResult = await userManager.AddToRoleAsync(userResult, roleResult.Name);
                        }
                    }
                    else
                    {
                        IdentityResult roleUserResult = await userManager.AddToRoleAsync(userResult, roleResult.Name);
                    }
                }
                //---Identity---

                if (!await db.Genres.AnyAsync())
                {
                    await db.Genres.AddAsync(new Genre
                    {
                        Name = "Film",
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.Genres.AddAsync(new Genre
                    {
                        Name = "Show",
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.Genres.AddAsync(new Genre
                    {
                        Name = "Action",
                        ParentId = 1,
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.Genres.AddAsync(new Genre
                    {
                        Name = "Adventure",
                        ParentId = 1,
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.Genres.AddAsync(new Genre
                    {
                        Name = "Comedy",
                        ParentId = 1,
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.Genres.AddAsync(new Genre
                    {
                        Name = "Romance",
                        ParentId = 1,
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.Genres.AddAsync(new Genre
                    {
                        Name = "Sci-Fi",
                        ParentId = 2,
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.Genres.AddAsync(new Genre
                    {
                        Name = "Horror",
                        ParentId = 2,
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.Genres.AddAsync(new Genre
                    {
                        Name = "Fantasy",
                        ParentId = 2,
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.SaveChangesAsync();
                }
                if (!await db.Faqs.AnyAsync())
                {
                    await db.Faqs.AddAsync(new Faq
                    {
                        Question = "YouVideo Nedir?",
                        Answer = "YouVideo istediğiniz zaman, istediğiniz yerden, birbirinden farklı dizi, film ve canlı yayını reklamsız izlemenizi sağlayan, Doğan Holding çatısı altında kurulmuş bir dijital televizyondur. İnternet bağlantısı ile bilgisayar, tablet, mobil cihazlar ya da akıllı televizyonlar (Smart TV)* üzerinden her ay güncellenen yerli/yabancı film ve dizilere, canlı TV yayınına, spor, yaşam içeriklerine ve en önemlisi sadece YouVideo'de bulabileceğiniz YouVideo üyelerine özel yerli yapımlara kolayca ulaşabilirsiniz.",
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.Faqs.AddAsync(new Faq
                    {
                        Question = "YouVideo’yi nasıl satın alabilirim?",
                        Answer = "YouVideo aboneliğinizi kredi/banka kartı bilgilerinizi sisteme girerek başlatabilirsiniz. Apple ürünlerinde (iPhone, iPad, AppleTV) uygulama içi satın alma (in-app purchase) seçeneği bulunmaktadır.",
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.Faqs.AddAsync(new Faq
                    {
                        Question = "YouVideo aboneliğine neler dahildir?",
                        Answer = "YouVideo, üyelerine istedikleri zaman, istedikleri yerden zengin bir içeriğe ulaşabilme özgürlüğü sunar. Bilgisayar, cep telefonu ve tablet gibi mobil cihazlar (iOS ve Android), akıllı TV (Smart TV), Apple TV ve Android Box cihazları üzerinden sürekli güncellenen içerikleri reklamsız sınırsız dilediğiniz kadar izlemenizi sağlar.",
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.Faqs.AddAsync(new Faq
                    {
                        Question = "YouVideo aboneliğimi nasıl iptal ederim?",
                        Answer = "Tek tuşla. YouVideo'de taahhüt yoktur. Dilediğiniz an, hesabım menüsü altından aboneliğinizi sonlandırabilirsiniz. İptal etmeniz durumunda herhangi bir cayma bedeli ödemezsiniz. Ödemesini yaptığınız dönemin sonunda aboneliğiniz durdurulur.",
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.Faqs.AddAsync(new Faq
                    {
                        Question = "YouVideo ile nasıl iletişime geçebilirim?",
                        Answer = "+994 70 891 13 00 iletişim numarasından veya destek@YouVideo.com adresinden bizlere ulaşabilirsiniz.",
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.SaveChangesAsync();
                }
                if (!await db.AppInfos.AnyAsync())
                {
                    await db.AppInfos.AddAsync(new AppInfo
                    {
                        Map = "<iframe src='https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3039.428490145605!2d49.85175681451219!3d40.37719496596853!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x40307d079efb5163%3A0xc20aa51a5f0b5e01!2sCode%20Academy!5e0!3m2!1saz!2s!4v1645018755931!5m2!1saz!2s' width='600' height='450' style='border: 0' allowfullscreen=''  loading='lazy'></iframe>",
                        HeaderContent = "You <span>video.</span>",
                        FooterContent = "You <span>video.</span>",
                        Description = "YouVideo is a software where you can rent movie & book tickets for shows.",
                        FacebookLink = "https://facebook.com",
                        TwitterLink = "https://twitter.com",
                        LinkedinLink = "https://linkedin.com",
                        TelegramLink = "https://telegram.org",
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.SaveChangesAsync();
                }
                if (!await db.Subscriptions.AnyAsync())
                {
                    await db.Subscriptions.AddAsync(new Subscription
                    {
                        Email = "zakir_rahimli@mail.ru",
                        IsConfirmed = true,
                        ConfirmationDate = DateTime.Parse("2022.03.18 05:32:03"),
                        CreatedDate = DateTime.UtcNow.AddHours(4)
                    });

                    await db.Subscriptions.AddAsync(new Subscription
                    {
                        Email = "zakirer@code.edu.az",
                        IsConfirmed = false,
                        CreatedDate = DateTime.UtcNow.AddHours(4)
                    });

                    await db.SaveChangesAsync();
                }
                if (!await db.ContactMessageTypes.AnyAsync())
                {
                    await db.ContactMessageTypes.AddAsync(new ContactMessageType
                    {
                        Text = "Film haqqında",
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.ContactMessageTypes.AddAsync(new ContactMessageType
                    {
                        Text = "Tamaşa haqqında",
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.ContactMessageTypes.AddAsync(new ContactMessageType
                    {
                        Text = "Tələb və ya təklif",
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.ContactMessageTypes.AddAsync(new ContactMessageType
                    {
                        Text = "Digər",
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.SaveChangesAsync();
                }
                if (!await db.ContactMessages.AnyAsync())
                {
                    await db.ContactMessages.AddAsync(new ContactMessage
                    {
                        Name = "Zakir",
                        Lastname = "Rahimli",
                        EmailAddress = "zakir_rahimli@mail.ru",
                        Content = "Salam, çox gözəl saytdır.",
                        ContactMessageTypeId = 4,
                        Answer = "Salam, çox sağolun.",
                        AnswerDate = new DateTime(2022, 3, 18, 5, 34, 21),
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                    });

                    await db.ContactMessages.AddAsync(new ContactMessage
                    {
                        Name = "Zakir",
                        Lastname = "Rahimli",
                        EmailAddress = "zrahimli93@gmail.com",
                        Content = "Salam, ilk tamaşa neçədədir?",
                        ContactMessageTypeId = 1,
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                    });

                    await db.SaveChangesAsync();
                }

                //---Shows---
                if (!await db.Directors.AnyAsync())
                {
                    await db.Directors.AddAsync(new Director
                    {
                        Name = "Joss Whedon",
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.Directors.AddAsync(new Director
                    {
                        Name = "David Lynch",
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.Directors.AddAsync(new Director
                    {
                        Name = "Anar Sadoqov",
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.SaveChangesAsync();
                }
                if (!await db.Casts.AnyAsync())
                {
                    await db.Casts.AddAsync(new Cast
                    {
                        Name = "Əli Əmirli",
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.Casts.AddAsync(new Cast
                    {
                        Name = "Siyavuş Kərimi",
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.Casts.AddAsync(new Cast
                    {
                        Name = "Mir Qabil Əkbərov",
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.Casts.AddAsync(new Cast
                    {
                        Name = "Fərid Vəliyev",
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.Casts.AddAsync(new Cast
                    {
                        Name = "Александр Фёдоров",
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.Casts.AddAsync(new Cast
                    {
                        Name = "Ольга Аббасова",
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.Casts.AddAsync(new Cast
                    {
                        Name = "Владимир Неверов",
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.Casts.AddAsync(new Cast
                    {
                        Name = "Chris Evans",
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.Casts.AddAsync(new Cast
                    {
                        Name = "Hayley Atwell",
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.Casts.AddAsync(new Cast
                    {
                        Name = "Timothy Olyphant",
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.Casts.AddAsync(new Cast
                    {
                        Name = "Robert Knepper",
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.Casts.AddAsync(new Cast
                    {
                        Name = "Robert Pettinson",
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.Casts.AddAsync(new Cast
                    {
                        Name = "Zoe Kravitz",
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.Casts.AddAsync(new Cast
                    {
                        Name = "Colin Farrol",
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.SaveChangesAsync();
                }
                if (!await db.Shows.AnyAsync())
                {
                    await db.Shows.AddAsync(new Show
                    {
                        Name = "Sürprizlər Günü",
                        Description = @"'День сюрпризов' по пьесе Валерия Мухарьямова. 
                                         Режиссёр постановщик - Александр Шаровский(Народный артист Азербайджана)
                                         Художник по декорациям - Александр Фёдоров(Заслуженный работник культуры Азербайджана)
                                         Художник по костюмам - Ольга Аббасова(Заслуженный работник культуры Азербайджана)
                                         Музыкальное оформление - Владимир Неверов(Заслуженный работник культуры Азербайджана)
                                         Хореография - Рауля Турккан
                                         Ассистент режиссера - Евгения Невмержицкая(Народная артистка Азербайджана)",
                        IsPremium = true,
                        Duration = "190 Min",
                        DirectorId = 4,
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.Shows.AddAsync(new Show
                    {
                        Name = "Ah, bu uzun sevda yolu",
                        Description = @"Anar Sadoqov",
                        IsPremium = false,
                        DirectorId = 6,
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.Shows.AddAsync(new Show
                    {
                        Name = "Kontrabas",
                        Description = "Kubizm yanaşması ilə qurulan bu tamaşada əsas mövzu kontrabasçını yəni qəhrəmanın ilk öncə kontrabasdan, ona olan sevgisindən danışıb sonradansa onu param parça etmək istəyindən ibarətdir. Lakin biz bunu alt qatında demək istəyirik ki, orkestr insan cəmiyyətinin eynisidir və nə olur olsun biz bu cəmiyyətdə hansısa bir alətdə ifa etməyə yəni yaşamağa məcburuq.",
                        IsPremium = true,
                        DirectorId = 3,
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.Shows.AddAsync(new Show
                    {
                        Name = "999-cu gecə",
                        Description = "Min bir gecə” nağıllarının motivləri əsasında hazırlanan “999-cu gecə” böyüklər üçün nağıl-tamaşasının süjet xəttinin əsasını çəkməçi Maruf la onun ifritə arvadı arasındakı təzadlı, ziddiyyətli münasibətlər təşkil edir. Maruf kasıb pinəçidir. Arvadı Fatma zənginlərin ayağını yuyur, bu əziyyətin heyfinisə ərindən çıxır-onu söyür döyür, incidir. Maruf əlini Allaha qaldırıb onu bu zülmdən qurtarmasını istəyir və qəfil şah sarayına düşür. Məlum olur ki, cəmi kasıbların var-dövlət arzusu o boyda padşahın arzusu ilə üst-üstə düşür. Rejissor tamaşanın finalında sevgini var-dövlətdən bir taxça yuxarı qoyur, obrazlar arasında fərq qoymadan, mənfi-müsbət obraz demədən, həyatın şərtlər içində oyun olduğuna eyham vurur. Yəni, Xeyir və Şər personajın mövcudluq çərçivəsinə çevrilmir, əməl xətti kimi təqdim olunur. Bu, bir tamaşa finalı üçün olduqca vacib məqamdır. Sonda “İfritə Fatma”nın da sevimli tanrı bəndəsi olduğunu yada salır. “Bu nağıl “1001 gecə”də Şəhrizadın danışdığı 999-cu nağıl olduğundan yaradıcı kollektiv tamaşanı “999-cu gecə” adlandırıb.",
                        IsPremium = false,
                        Duration = "150 Min",
                        DirectorId = 6,
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.SaveChangesAsync();
                }
                if (!await db.ShowGenreCastItems.AnyAsync())
                {
                    await db.ShowGenreCastItems.AddAsync(new ShowGenreCastItem
                    {
                        ShowId = 1,
                        GenreId = 7,
                        CastId = 5,
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.ShowGenreCastItems.AddAsync(new ShowGenreCastItem
                    {
                        ShowId = 1,
                        GenreId = 8,
                        CastId = 6,
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.ShowGenreCastItems.AddAsync(new ShowGenreCastItem
                    {
                        ShowId = 1,
                        GenreId = 9,
                        CastId = 7,
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.ShowGenreCastItems.AddAsync(new ShowGenreCastItem
                    {
                        ShowId = 2,
                        GenreId = 7,
                        CastId = 1,
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.ShowGenreCastItems.AddAsync(new ShowGenreCastItem
                    {
                        ShowId = 2,
                        GenreId = 9,
                        CastId = 2,
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.ShowGenreCastItems.AddAsync(new ShowGenreCastItem
                    {
                        ShowId = 3,
                        GenreId = 8,
                        CastId = 3,
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.ShowGenreCastItems.AddAsync(new ShowGenreCastItem
                    {
                        ShowId = 3,
                        GenreId = 9,
                        CastId = 4,
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.ShowGenreCastItems.AddAsync(new ShowGenreCastItem
                    {
                        ShowId = 4,
                        GenreId = 9,
                        CastId = 2,
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.ShowGenreCastItems.AddAsync(new ShowGenreCastItem
                    {
                        ShowId = 4,
                        GenreId = 8,
                        CastId = 3,
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.SaveChangesAsync();
                }
                //---Shows---

                //---Movie---
                if (!await db.Movies.AnyAsync())
                {
                    await db.Movies.AddAsync(new Movie
                    {
                        Name = "Captain America",
                        Description = "Captain America is a superhero appearing in American comic books published by Marvel Comics. Created by cartoonists Joe Simon and Jack Kirby, the character first appeared in Captain America Comics #1 (cover dated March 1941) from Timely Comics, a predecessor of Marvel Comics. ",
                        MovieFrame = "<iframe width='560' height='315' src='https://www.youtube.com/embed/dE1P4zDhhqw' title='YouTube video player' frameborder='0' allow='accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture' allowfullscreen></iframe>",
                        IsPremium = false,
                        Price = 3.00M,
                        Duration = "180 Min",
                        DirectorId = 4,
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.Movies.AddAsync(new Movie
                    {
                        Name = "Hitman",
                        Description = "Hitman is a 2007 action-thriller film directed by Xavier Gens and based on the video game series of the same name. The story revolves around Agent 47, a professional hitman, who was engineered to be an assassin by the group known as 'ICA'(International Contract Agency).",
                        MovieFrame = "<iframe width='560' height='315' src='https://www.youtube.com/embed/pAMy7IhOVQE' title='YouTube video player' frameborder='0' allow='accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture' allowfullscreen></iframe>",
                        IsPremium = true,
                        Price = 7.00M,
                        Quality = "HD",
                        DirectorId = 3,
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.Movies.AddAsync(new Movie
                    {
                        Name = "BATMAN",
                        Description = "The Batman is a 2022 American superhero film based on the DC Comics character Batman. Produced by DC Films, 6th & Idaho, and Dylan Clark Productions, and distributed by Warner Bros. Pictures, it is a reboot of the Batman film franchise.",
                        MovieFrame = "<iframe width='560' height='315' src='https://www.youtube.com/embed/mqqft2x_Aa4' title='YouTube video player' frameborder='0' allow='accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture' allowfullscreen></iframe>",
                        IsPremium = true,
                        Price = 5.00M,
                        Duration = "200 Min",
                        Quality = "HD",
                        DirectorId = 4,
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.SaveChangesAsync();
                }
                if (!await db.MovieGenreCastItems.AnyAsync())
                {
                    await db.MovieGenreCastItems.AddAsync(new MovieGenreCastItem
                    {
                        MovieId = 1,
                        GenreId = 3,
                        CastId = 8,
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.MovieGenreCastItems.AddAsync(new MovieGenreCastItem
                    {
                        MovieId = 1,
                        GenreId = 4,
                        CastId = 9,
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.MovieGenreCastItems.AddAsync(new MovieGenreCastItem
                    {
                        MovieId = 2,
                        GenreId = 5,
                        CastId = 10,
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.MovieGenreCastItems.AddAsync(new MovieGenreCastItem
                    {
                        MovieId = 2,
                        GenreId = 6,
                        CastId = 11,
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.MovieGenreCastItems.AddAsync(new MovieGenreCastItem
                    {
                        MovieId = 3,
                        GenreId = 3,
                        CastId = 12,
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.MovieGenreCastItems.AddAsync(new MovieGenreCastItem
                    {
                        MovieId = 3,
                        GenreId = 4,
                        CastId = 13,
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.MovieGenreCastItems.AddAsync(new MovieGenreCastItem
                    {
                        MovieId = 3,
                        GenreId = 6,
                        CastId = 14,
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.SaveChangesAsync();
                }
                //---Movie---

                //---Blogs---
                if (!await db.BlogImages.AnyAsync())
                {
                    await db.BlogImages.AddAsync(new BlogImage
                    {
                        ImagePath = "blog-36123afea37b49f298908b98420a60db.jpg",
                        IsMain = true,
                        BlogId = 13
                    });

                    await db.BlogImages.AddAsync(new BlogImage
                    {
                        ImagePath = "blog-f4911199640444f1bd4f89b8e1081ed0.jpg",
                        IsMain = false,
                        BlogId = 13
                    });

                    await db.BlogImages.AddAsync(new BlogImage
                    {
                        ImagePath = "blog-5aae0a1908d441a5baec65a62bee14c8.jpg",
                        IsMain = false,
                        BlogId = 13
                    });

                    await db.BlogImages.AddAsync(new BlogImage
                    {
                        ImagePath = "blog-d1fce48e21d044c5b6b807d08790629f.jpg",
                        IsMain = true,
                        BlogId = 14
                    });

                    await db.BlogImages.AddAsync(new BlogImage
                    {
                        ImagePath = "blog-3e3bc2ef81134ac38d0d246491e13628.jpeg",
                        IsMain = false,
                        BlogId = 14
                    });

                    await db.BlogImages.AddAsync(new BlogImage
                    {
                        ImagePath = "blog-d7791fbdfa6248629a535221e9856cf5.jpg",
                        IsMain = false,
                        BlogId = 14
                    });

                    await db.BlogImages.AddAsync(new BlogImage
                    {
                        ImagePath = "blog-2f84898479f64a2cb5642f469fde69a3.jpeg",
                        IsMain = false,
                        BlogId = 15
                    });

                    await db.BlogImages.AddAsync(new BlogImage
                    {
                        ImagePath = "blog-7d852c08ad5841f3a56ca01af48f5e7b.jpg",
                        IsMain = true,
                        BlogId = 15
                    });

                    await db.BlogImages.AddAsync(new BlogImage
                    {
                        ImagePath = "blog-e2fe18b4daf3457a8fda2f9d9698949f.webp",
                        IsMain = false,
                        BlogId = 15
                    });

                    await db.SaveChangesAsync();
                }
                if (!await db.Blogs.AnyAsync())
                {
                    await db.Blogs.AddAsync(new Blog
                    {
                        Title = "Hellbound",
                        Description = "Hellbound (Korean: 지옥; Hanja: 地獄; RR: Jiok) is a South Korean dark fantasy streaming television series directed by Yeon Sang-ho, based on his own webtoon of the same name. An original Netflix release about supernatural beings appearing out of nowhere to condemn people to Hell, the series stars Yoo Ah-in, Kim Hyun-joo, Park Jeong-min, Won Jin-ah and Yang Ik-june.",
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.Blogs.AddAsync(new Blog
                    {
                        Title = "Squid Game",
                        Description = "Squid Game (Korean: 오징어 게임; RR: Ojing-eo Geim) is a South Korean survival drama television series created by Hwang Dong-hyuk for Netflix. Its cast includes Lee Jung-jae, Park Hae-soo, Wi Ha-joon, HoYeon Jung, O Yeong-su, Heo Sung-tae, Anupam Tripathi, and Kim Joo-ryoung.",
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.Blogs.AddAsync(new Blog
                    {
                        Title = "Game of Thrones",
                        Description = "Game of Thrones is an American fantasy drama television series created by David Benioff and D. B. Weiss for HBO. It is an adaptation of A Song of Ice and Fire, a series of fantasy novels by George R. R. Martin, the first of which is A Game of Thrones. The show was shot in the United Kingdom, Canada, Croatia, Iceland, Malta, Morocco, and Spain. It premiered on HBO in the United States on April 17, 2011, and concluded on May 19, 2019, with 73 episodes broadcast over eight seasons.",
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.SaveChangesAsync();
                }
                //---Blogs---
            }

            return app;
        }
    }
}
