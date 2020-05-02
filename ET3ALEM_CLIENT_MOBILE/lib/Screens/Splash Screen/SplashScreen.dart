import 'package:flutter/material.dart';
import 'package:font_awesome_flutter/font_awesome_flutter.dart';

class SplashScreen extends StatelessWidget {
  const SplashScreen({Key key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return SafeArea(
      child: Scaffold(
        body: SingleChildScrollView(
          child: Padding(
              padding: EdgeInsets.symmetric(vertical: 10, horizontal: 20),
              child: Column(
                mainAxisAlignment: MainAxisAlignment.start,
                children: <Widget>[
                  Container(
                    height: MediaQuery.of(context).size.height * (2 / 3) - 20,
                    width: double.infinity,
                    child: Column(
                      mainAxisAlignment: MainAxisAlignment.center,
                      children: <Widget>[
                        Text(
                          'Welcome to ET3ALEM',
                          style: TextStyle(
                              color: Colors.black,
                              fontSize: 60,
                              fontWeight: FontWeight.bold),
                        ),
                        Text(
                          'We are happy to provide the education you need',
                          style: TextStyle(fontSize: 45),
                        ),
                      ],
                    ),
                  ),
                  Container(
                    width: double.infinity,
                    height: MediaQuery.of(context).size.height * (1 / 3) - 20,
                    child: Column(
                      mainAxisAlignment: MainAxisAlignment.center,
                      children: <Widget>[
                        SizedBox(
                          width: double.infinity,
                          child: FlatButton(
                            onPressed: () {},
                            child: Text(
                              'Register',
                              style:
                                  TextStyle(color: Colors.white, fontSize: 30),
                            ),
                            padding: EdgeInsets.symmetric(
                                horizontal: 30, vertical: 15),
                            color: Theme.of(context).primaryColor,
                            shape: new RoundedRectangleBorder(
                                borderRadius: new BorderRadius.circular(15)),
                          ),
                        ),
                        SizedBox(
                          height: 20,
                        ),
                        SizedBox(
                          width: double.infinity,
                          child: FlatButton(
                            onPressed: () {},
                            child: Text(
                              'Log in',
                              style:
                                  TextStyle(color: Colors.white, fontSize: 30),
                            ),
                            padding: EdgeInsets.symmetric(
                                horizontal: 30, vertical: 15),
                            color: Theme.of(context).accentColor,
                            shape: new RoundedRectangleBorder(
                                borderRadius: new BorderRadius.circular(15)),
                          ),
                        ),
                      ],
                    ),
                  ),
                ],
              )),
        ),
      ),
    );
  }
}
