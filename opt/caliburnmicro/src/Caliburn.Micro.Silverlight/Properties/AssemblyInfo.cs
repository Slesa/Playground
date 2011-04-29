﻿using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Markup;

[assembly: AssemblyTitle("Caliburn Micro")]
[assembly: AssemblyDescription("A small, yet powerful framework designed for WPF, Silverlight and WP7. It implements a variety of UI patterns for solving real-world problems. Patterns that are highlighted include MVVM (Presentation Model), MVP and MVC.")]
[assembly: AssemblyProduct("Caliburn.Micro")]
[assembly: AssemblyCompany("Blue Spire Consulting, Inc.")]
[assembly: AssemblyCopyright("Copyright © 2010")]
[assembly: ComVisible(false)]
[assembly: Guid("6449e9cb-4d4d-4d79-8f73-71a2fc647109")]
[assembly: AssemblyVersion("1.1.0.0")]
[assembly: AssemblyFileVersion("1.1.0.0")]

[assembly: XmlnsDefinition("http://www.caliburnproject.org", "Caliburn.Micro")]
[assembly: XmlnsPrefix("http://www.caliburnproject.org", "cal")]

#if !SILVERLIGHT
[assembly: CLSCompliant(true)]
#endif