﻿using Microsoft.Extensions.Logging;
using SygehusKoordinering.View;
using SygehusKoordinering.ViewModel;

namespace SygehusKoordinering
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainViewModel>();

            builder.Services.AddTransient<LoginView>();
            builder.Services.AddTransient<LoginViewModel>();

            builder.Services.AddTransient<OplysningView>();
            builder.Services.AddTransient<OplysningViewModel>();

            builder.Services.AddTransient<OprettelseBookingView>();
            builder.Services.AddTransient<OprettelseBookingViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
