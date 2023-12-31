﻿using DesignPatterns.Structural.Adapter.Adaption;
using DesignPatterns.Structural.Adapter.Interface;
using DesignPatterns.Structural.Adapter.Legacy;
using DesignPatterns.Structural.Bridge.Abstraction;
using DesignPatterns.Structural.Bridge.Implementation;
using DesignPatterns.Structural.Composite.Component;
using DesignPatterns.Structural.Composite.Composite;
using DesignPatterns.Structural.Composite.Entities;
using DesignPatterns.Structural.Decorator.Decorators;
using DesignPatterns.Structural.Decorator.Entities;
using DesignPatterns.Structural.Decorator.Interface;
using DesignPatterns.Structural.Facade.System;
using DesignPatterns.Structural.Flyweight.Factory;
using DesignPatterns.Structural.Proxy.Implementation;
using DesignPatterns.Structural.Proxy.Interface;

namespace DesignPatterns.Structural
{
    public static class ExamplesStructural
    {
        public static void Decorator()
        {
            // Default behavior
            ITextFormatter plainFormatter = new PlainTextFormatter();
            Console.WriteLine(plainFormatter.Format("This is text."));

            // Add remove space behavior
            ITextFormatter removeSpaceFormatter = new RemoveSpaceDecorator(plainFormatter);
            Console.WriteLine(removeSpaceFormatter.Format("This is text."));

            // Add to upper behaviour
            ITextFormatter removeAndUpperFormmater = new ToUpperDecorator(removeSpaceFormatter);
            Console.WriteLine(removeAndUpperFormmater.Format("This is text."));
        }

        public static void Facade()
        {
            // The ideia of a facade is actually hide the complexity, take a look inside
            IPeopleConsult consultService = new PeopleConsult();
            consultService.GetPeople(2);
        }

        public static void Adapter()
        {
            // Legacy code, not compatible at all and can't modify
            // We need to adapt to the new interface that the system expect
            CelsiusTemperatureProvider temperatureProvider = new CelsiusTemperatureProvider();

            // Adapter to new system interface
            ITemperatureProvider kelvinTemperatureProvider = new KelvinTemperatureAdapter(temperatureProvider);
            ITemperatureProvider celsiusTemperatureProvider = new CelsiusTemperatureAdapter(temperatureProvider);
            ITemperatureProvider fahrenheitTemperatureProvider = new FahrenheitTemperatureAdapter(temperatureProvider);

            kelvinTemperatureProvider.GetTemperature();
            celsiusTemperatureProvider.GetTemperature();
            fahrenheitTemperatureProvider.GetTemperature();
        }

        public static void Composite()
        {
            // Create a tree of shapes to draw and calculate total area
            IShape rect1 = new Rectangle(10, 10);
            IShape rect2 = new Rectangle(5, 5);

            IShape circle1 = new Circle(2);
            IShape circle2 = new Circle(2);

            GroupOfShapes groupRoot = new GroupOfShapes();
            GroupOfShapes groupRootRects = new GroupOfShapes();
            GroupOfShapes groupRootCircles = new GroupOfShapes();

            groupRoot.Add(groupRootCircles);
            groupRoot.Add(groupRootRects);

            groupRootRects.Add(rect1);
            groupRootRects.Add(rect2);

            groupRootCircles.Add(circle1);
            groupRootCircles.Add(circle2);
               
            // Draw all group
            groupRoot.Draw();

            // Calculate total area and per type
            double totalArea = groupRoot.CalculateArea();
            double totalRects = groupRootRects.CalculateArea();
            double totalCircles = groupRootCircles.CalculateArea();

            Console.WriteLine($"Total area to draw {totalArea}");
            Console.WriteLine($"Total area to draw of circles {totalCircles}");
            Console.WriteLine($"Total area to draw of rects {totalRects}");
        }

        public static void Flyweight()
        {
            AirplaneFactory airplaneFactory = new AirplaneFactory();

            // Plane 1 and Plane2 will share the same intrisic state
            var plane1 = airplaneFactory.CreateAirPlane(100, 200, 10, "airplane1.jpg", "red");
            var plane2 = airplaneFactory.CreateAirPlane(250, 100, 5, "airplane1.jpg", "red");

            // New instricit state here
            var plane3 = airplaneFactory.CreateAirPlane(250, 100, 5, "airplane2.jpg", "blue");
        }

        public static void Bridge()
        {
            IGuiRenderer windowsRenderer = new WindowsGuiRenderer();
            IGuiRenderer linuxRenderer = new LinuxGuiRenderer();

            GuiElement linuxTextBox = new TextBox(linuxRenderer, "Enter your name");
            GuiElement windowsButton = new Button(windowsRenderer, "OK");

            windowsButton.Render();
            linuxTextBox.Render();
        }

        public static void Proxy()
        {
            IImage image1 = new ProxyImage("image1.jpg");
            IImage image2 = new ProxyImage("image2.jpg");

            // Real loading occurs only when Display is called
            image1.Display();
            image2.Display();
        }
    }
}
