import 'package:flutter/material.dart';

import 'Helpers/Themes/LightThemes.dart';
import 'Screens/Splash Screen/SplashScreen.dart';

void main() => runApp(MyApp());

class MyApp extends StatelessWidget {
  const MyApp({Key key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'ET3ALEM',
      theme: blueRedTheme,
      home: SplashScreen(),
      debugShowCheckedModeBanner: false,
    );
  }
}
