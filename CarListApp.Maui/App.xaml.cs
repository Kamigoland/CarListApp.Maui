﻿using CarListApp.Maui.Models;
using CarListApp.Maui.Services;

namespace CarListApp.Maui;

public partial class App : Application
{
	public static UserInfo UserInfo;
	public static CarDatabaseService CarDatabaseService { get; private set; }
	public App(CarDatabaseService carService)
	{
		InitializeComponent();

		MainPage = new AppShell();
		CarDatabaseService = carService;
	}
}
