OwinSslMiddleware
==========

##<a name="intro"></a> SSL Middleware für Owin und SelfHost

Die Middleware sorgt dafür, dass alle Anfragen die von HTTP kommen, umgeleitet werden auf HTTPS. Somit kann man immer sicher sein, dass alle Anfragen über HTTPS laufen.

##<a name="example"></a> Beispiel
 
```c#
public void Configuration(IAppBuilder appBuilder)
{
	var httpConfiguration = new HttpConfiguration();
	//Middleware muss vor allen anderen Middlewares laufen, sodass jede Anfrage über die SSL Middleware läuft
	appBuilder.UseSsl(443);
	appBuilder.UseWebApi(httpConfiguration));
}
```