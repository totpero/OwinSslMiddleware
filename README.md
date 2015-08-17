OwinSslMiddleware
==========

##<a name="intro"></a> SSL Middleware f�r Owin und SelfHost

Die Middleware sorgt daf�r, dass alle Anfragen die von HTTP kommen, umgeleitet werden auf HTTPS. Somit kann man immer sicher sein, dass alle Anfragen �ber HTTPS laufen.

##<a name="example"></a> Beispiel
 
```c#
public void Configuration(IAppBuilder appBuilder)
{
	var httpConfiguration = new HttpConfiguration();
	//Middleware muss vor allen anderen Middlewares laufen, sodass jede Anfrage �ber die SSL Middleware l�uft
	appBuilder.UseSsl(443);
	appBuilder.UseWebApi(httpConfiguration));
}
```