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
                        Answer = "YouVideo istedi??iniz zaman, istedi??iniz yerden, birbirinden farkl?? dizi, film ve canl?? yay??n?? reklams??z izlemenizi sa??layan, Do??an Holding ??at??s?? alt??nda kurulmu?? bir dijital televizyondur. ??nternet ba??lant??s?? ile bilgisayar, tablet, mobil cihazlar ya da ak??ll?? televizyonlar (Smart TV)* ??zerinden her ay g??ncellenen yerli/yabanc?? film ve dizilere, canl?? TV yay??n??na, spor, ya??am i??eriklerine ve en ??nemlisi sadece YouVideo'de bulabilece??iniz YouVideo ??yelerine ??zel yerli yap??mlara kolayca ula??abilirsiniz.",
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.Faqs.AddAsync(new Faq
                    {
                        Question = "YouVideo???yi nas??l sat??n alabilirim?",
                        Answer = "YouVideo aboneli??inizi kredi/banka kart?? bilgilerinizi sisteme girerek ba??latabilirsiniz. Apple ??r??nlerinde (iPhone, iPad, AppleTV) uygulama i??i sat??n alma (in-app purchase) se??ene??i bulunmaktad??r.",
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.Faqs.AddAsync(new Faq
                    {
                        Question = "YouVideo aboneli??ine neler dahildir?",
                        Answer = "YouVideo, ??yelerine istedikleri zaman, istedikleri yerden zengin bir i??eri??e ula??abilme ??zg??rl?????? sunar. Bilgisayar, cep telefonu ve tablet gibi mobil cihazlar (iOS ve Android), ak??ll?? TV (Smart TV), Apple TV ve Android Box cihazlar?? ??zerinden s??rekli g??ncellenen i??erikleri reklams??z s??n??rs??z diledi??iniz kadar izlemenizi sa??lar.",
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.Faqs.AddAsync(new Faq
                    {
                        Question = "YouVideo aboneli??imi nas??l iptal ederim?",
                        Answer = "Tek tu??la. YouVideo'de taahh??t yoktur. Diledi??iniz an, hesab??m men??s?? alt??ndan aboneli??inizi sonland??rabilirsiniz. ??ptal etmeniz durumunda herhangi bir cayma bedeli ??demezsiniz. ??demesini yapt??????n??z d??nemin sonunda aboneli??iniz durdurulur.",
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.Faqs.AddAsync(new Faq
                    {
                        Question = "YouVideo ile nas??l ileti??ime ge??ebilirim?",
                        Answer = "+994 70 891 13 00 ileti??im numaras??ndan veya destek@YouVideo.com adresinden bizlere ula??abilirsiniz.",
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
                        Text = "Film haqq??nda",
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.ContactMessageTypes.AddAsync(new ContactMessageType
                    {
                        Text = "Tama??a haqq??nda",
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.ContactMessageTypes.AddAsync(new ContactMessageType
                    {
                        Text = "T??l??b v?? ya t??klif",
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.ContactMessageTypes.AddAsync(new ContactMessageType
                    {
                        Text = "Dig??r",
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
                        Content = "Salam, ??ox g??z??l saytd??r.",
                        ContactMessageTypeId = 4,
                        Answer = "Salam, ??ox sa??olun.",
                        AnswerDate = new DateTime(2022, 3, 18, 5, 34, 21),
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                    });

                    await db.ContactMessages.AddAsync(new ContactMessage
                    {
                        Name = "Zakir",
                        Lastname = "Rahimli",
                        EmailAddress = "zrahimli93@gmail.com",
                        Content = "Salam, ilk tama??a ne????d??dir?",
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
                if (!await db.Rooms.AnyAsync())
                {
                    await db.Rooms.AddAsync(new Room
                    {
                        Name = "Moon",
                        CreatedByUserId = 1,
                        CreatedDate = DateTime.UtcNow.AddHours(4)
                    });

                    await db.Rooms.AddAsync(new Room
                    {
                        Name = "Granit",
                        CreatedByUserId = 1,
                        CreatedDate = DateTime.UtcNow.AddHours(4)
                    });

                    await db.SaveChangesAsync();
                }
                if (!await db.Seats.AnyAsync())
                {
                    for (int i = 0; i < 60; i++)
                    {
                        await db.Seats.AddAsync(new Seat
                        {
                            RoomId = 1,
                            CreatedByUserId = 1,
                            CreatedDate = DateTime.UtcNow.AddHours(4)
                        });
                    }

                    for (int i = 0; i < 70; i++)
                    {
                        await db.Seats.AddAsync(new Seat
                        {
                            RoomId = 2,
                            CreatedByUserId = 1,
                            CreatedDate = DateTime.UtcNow.AddHours(4)
                        });
                    }

                    await db.SaveChangesAsync();
                }
                if (!await db.Casts.AnyAsync())
                {
                    await db.Casts.AddAsync(new Cast
                    {
                        Name = "??li ??mirli",
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.Casts.AddAsync(new Cast
                    {
                        Name = "Siyavu?? K??rimi",
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.Casts.AddAsync(new Cast
                    {
                        Name = "Mir Qabil ??kb??rov",
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.Casts.AddAsync(new Cast
                    {
                        Name = "F??rid V??liyev",
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.Casts.AddAsync(new Cast
                    {
                        Name = "?????????????????? ??????????????",
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.Casts.AddAsync(new Cast
                    {
                        Name = "?????????? ????????????????",
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.Casts.AddAsync(new Cast
                    {
                        Name = "???????????????? ??????????????",
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
                        Name = "S??rprizl??r G??n??",
                        Description = @"'???????? ??????????????????' ???? ?????????? ?????????????? ??????????????????????. 
                                         ???????????????? ?????????????????????? - ?????????????????? ??????????????????(???????????????? ???????????? ????????????????????????)
                                         ???????????????? ???? ???????????????????? - ?????????????????? ??????????????(?????????????????????? ???????????????? ???????????????? ????????????????????????)
                                         ???????????????? ???? ???????????????? - ?????????? ????????????????(?????????????????????? ???????????????? ???????????????? ????????????????????????)
                                         ?????????????????????? ???????????????????? - ???????????????? ??????????????(?????????????????????? ???????????????? ???????????????? ????????????????????????)
                                         ?????????????????????? - ?????????? ??????????????
                                         ?????????????????? ?????????????????? - ?????????????? ????????????????????????(???????????????? ???????????????? ????????????????????????)",
                        IsPremium = true,
                        ImagePath = "show-e7e194e2f9934bbdbd516cbee23ce22d.jpg",
                        Duration = "190 Min",
                        DirectorId = 1,
                        RoomId = 1,
                        Price = 3,
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.Shows.AddAsync(new Show
                    {
                        Name = "Ah, bu uzun sevda yolu",
                        Description = @"Anar Sadoqov",
                        IsPremium = false,
                        ImagePath = "show-bc2aafef1d4d4d43b40dc2bdb38bd915.jpg",
                        DirectorId = 2,
                        RoomId = 2,
                        Price = 7,
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.Shows.AddAsync(new Show
                    {
                        Name = "Kontrabas",
                        Description = "Kubizm yana??mas?? il?? qurulan bu tama??ada ??sas m??vzu kontrabas????n?? y??ni q??hr??man??n ilk ??nc?? kontrabasdan, ona olan sevgisind??n dan??????b sonradansa onu param par??a etm??k ist??yind??n ibar??tdir. Lakin biz bunu alt qat??nda dem??k ist??yirik ki, orkestr insan c??miyy??tinin eynisidir v?? n?? olur olsun biz bu c??miyy??td?? hans??sa bir al??td?? ifa etm??y?? y??ni ya??ama??a m??cburuq.",
                        IsPremium = true,
                        ImagePath = "show-aa37a5cab9cb4dfe9db56c3a453be12c.jpg",
                        DirectorId = 3,
                        RoomId = 1,
                        Price = 9,
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.Shows.AddAsync(new Show
                    {
                        Name = "999-cu gec??",
                        Description = "Min bir gec????? na????llar??n??n motivl??ri ??sas??nda haz??rlanan ???999-cu gec????? b??y??kl??r ??????n na????l-tama??as??n??n s??jet x??ttinin ??sas??n?? ????km????i Maruf la onun ifrit?? arvad?? aras??ndak?? t??zadl??, ziddiyy??tli m??nasib??tl??r t????kil edir. Maruf kas??b pin????idir. Arvad?? Fatma z??nginl??rin aya????n?? yuyur, bu ??ziyy??tin heyfinis?? ??rind??n ????x??r-onu s??y??r d??y??r, incidir. Maruf ??lini Allaha qald??r??b onu bu z??lmd??n qurtarmas??n?? ist??yir v?? q??fil ??ah saray??na d??????r. M??lum olur ki, c??mi kas??blar??n var-d??vl??t arzusu o boyda pad??ah??n arzusu il?? ??st-??st?? d??????r. Rejissor tama??an??n final??nda sevgini var-d??vl??td??n bir tax??a yuxar?? qoyur, obrazlar aras??nda f??rq qoymadan, m??nfi-m??sb??t obraz dem??d??n, h??yat??n ????rtl??r i??ind?? oyun oldu??una eyham vurur. Y??ni, Xeyir v?? ????r personaj??n m??vcudluq ????r??iv??sin?? ??evrilmir, ??m??l x??tti kimi t??qdim olunur. Bu, bir tama??a final?? ??????n olduqca vacib m??qamd??r. Sonda ?????frit?? Fatma???n??n da sevimli tanr?? b??nd??si oldu??unu yada sal??r. ???Bu na????l ???1001 gec?????d?? ????hrizad??n dan????d?????? 999-cu na????l oldu??undan yarad??c?? kollektiv tama??an?? ???999-cu gec????? adland??r??b.",
                        IsPremium = false,
                        ImagePath = "show-e94c73f5959c4c82ab69b4348cce776c.jpg",
                        Duration = "150 Min",
                        DirectorId = 1,
                        RoomId = 2,
                        Price = 5,
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
                        GenreId = 1,
                        CastId = 1,
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.ShowGenreCastItems.AddAsync(new ShowGenreCastItem
                    {
                        ShowId = 1,
                        GenreId = 2,
                        CastId = 2,
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.ShowGenreCastItems.AddAsync(new ShowGenreCastItem
                    {
                        ShowId = 1,
                        GenreId = 3,
                        CastId = 3,
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.ShowGenreCastItems.AddAsync(new ShowGenreCastItem
                    {
                        ShowId = 2,
                        GenreId = 1,
                        CastId = 1,
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.ShowGenreCastItems.AddAsync(new ShowGenreCastItem
                    {
                        ShowId = 2,
                        GenreId = 2,
                        CastId = 2,
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.ShowGenreCastItems.AddAsync(new ShowGenreCastItem
                    {
                        ShowId = 3,
                        GenreId = 3,
                        CastId = 3,
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.ShowGenreCastItems.AddAsync(new ShowGenreCastItem
                    {
                        ShowId = 3,
                        GenreId = 4,
                        CastId = 4,
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.ShowGenreCastItems.AddAsync(new ShowGenreCastItem
                    {
                        ShowId = 4,
                        GenreId = 2,
                        CastId = 2,
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.ShowGenreCastItems.AddAsync(new ShowGenreCastItem
                    {
                        ShowId = 4,
                        GenreId = 3,
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
                        DirectorId = 3,
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
                        DirectorId = 1,
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
                        GenreId = 7,
                        CastId = 1,
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.MovieGenreCastItems.AddAsync(new MovieGenreCastItem
                    {
                        MovieId = 1,
                        GenreId = 8,
                        CastId = 1,
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.MovieGenreCastItems.AddAsync(new MovieGenreCastItem
                    {
                        MovieId = 2,
                        GenreId = 8,
                        CastId = 2,
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.MovieGenreCastItems.AddAsync(new MovieGenreCastItem
                    {
                        MovieId = 2,
                        GenreId = 9,
                        CastId = 3,
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.MovieGenreCastItems.AddAsync(new MovieGenreCastItem
                    {
                        MovieId = 3,
                        GenreId = 7,
                        CastId = 3,
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.MovieGenreCastItems.AddAsync(new MovieGenreCastItem
                    {
                        MovieId = 3,
                        GenreId = 8,
                        CastId = 3,
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.MovieGenreCastItems.AddAsync(new MovieGenreCastItem
                    {
                        MovieId = 3,
                        GenreId = 8,
                        CastId = 4,
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.SaveChangesAsync();
                }
                //---Movie---

                //---Blogs---
                if (!await db.Blogs.AnyAsync())
                {
                    await db.Blogs.AddAsync(new Blog
                    {
                        Title = "Hellbound",
                        Description = "Hellbound (Korean: ??????; Hanja: ??????; RR: Jiok) is a South Korean dark fantasy streaming television series directed by Yeon Sang-ho, based on his own webtoon of the same name. An original Netflix release about supernatural beings appearing out of nowhere to condemn people to Hell, the series stars Yoo Ah-in, Kim Hyun-joo, Park Jeong-min, Won Jin-ah and Yang Ik-june.",
                        CreatedDate = DateTime.UtcNow.AddHours(4),
                        CreatedByUserId = 1
                    });

                    await db.Blogs.AddAsync(new Blog
                    {
                        Title = "Squid Game",
                        Description = "Squid Game (Korean: ????????? ??????; RR: Ojing-eo Geim) is a South Korean survival drama television series created by Hwang Dong-hyuk for Netflix. Its cast includes Lee Jung-jae, Park Hae-soo, Wi Ha-joon, HoYeon Jung, O Yeong-su, Heo Sung-tae, Anupam Tripathi, and Kim Joo-ryoung.",
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

                if (!await db.BlogImages.AnyAsync())
                {
                    await db.BlogImages.AddAsync(new BlogImage
                    {
                        ImagePath = "blog-36123afea37b49f298908b98420a60db.jpg",
                        IsMain = true,
                        BlogId = 1
                    });

                    await db.BlogImages.AddAsync(new BlogImage
                    {
                        ImagePath = "blog-f4911199640444f1bd4f89b8e1081ed0.jpg",
                        IsMain = false,
                        BlogId = 1
                    });

                    await db.BlogImages.AddAsync(new BlogImage
                    {
                        ImagePath = "blog-5aae0a1908d441a5baec65a62bee14c8.jpg",
                        IsMain = false,
                        BlogId = 1
                    });

                    await db.BlogImages.AddAsync(new BlogImage
                    {
                        ImagePath = "blog-d1fce48e21d044c5b6b807d08790629f.jpg",
                        IsMain = true,
                        BlogId = 2
                    });

                    await db.BlogImages.AddAsync(new BlogImage
                    {
                        ImagePath = "blog-3e3bc2ef81134ac38d0d246491e13628.jpeg",
                        IsMain = false,
                        BlogId = 2
                    });

                    await db.BlogImages.AddAsync(new BlogImage
                    {
                        ImagePath = "blog-d7791fbdfa6248629a535221e9856cf5.jpg",
                        IsMain = false,
                        BlogId = 2
                    });

                    await db.BlogImages.AddAsync(new BlogImage
                    {
                        ImagePath = "blog-2f84898479f64a2cb5642f469fde69a3.jpeg",
                        IsMain = false,
                        BlogId = 3
                    });

                    await db.BlogImages.AddAsync(new BlogImage
                    {
                        ImagePath = "blog-7d852c08ad5841f3a56ca01af48f5e7b.jpg",
                        IsMain = true,
                        BlogId = 3
                    });

                    await db.BlogImages.AddAsync(new BlogImage
                    {
                        ImagePath = "blog-e2fe18b4daf3457a8fda2f9d9698949f.webp",
                        IsMain = false,
                        BlogId = 3
                    });

                    await db.SaveChangesAsync();
                }
                //---Blogs---
            }

            return app;
        }
    }
}
