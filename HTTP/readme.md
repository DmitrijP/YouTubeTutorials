# DIY HTTP Server

Youtube Video: https://youtu.be/LNMvuUUUbMg

Ziel dieser Dokumentation ist es einen eigenen HTTP Server zu bauen und zu verstehen wie die Kommunikation zwischen Server und Browser/Client ablauft.
 
--- 
## Sockets

Wir fangen mit einer einfachen Anwendung an die ein TCP Socket aufbaut über den es Daten empfangen und anzeigen kann.

```java
public class Server {  
    public static void main(String[] args){  
        try {  
            ServerSocket serverSocket = new ServerSocket(8081);  
            Socket acceptedConnection = serverSocket.accept();  
            OutputStream out = acceptedConnection.getOutputStream();  
            BufferedReader in = new BufferedReader(new InputStreamReader(  
                    acceptedConnection.getInputStream()));  
            System.out.println(in.readLine());  
            out.write("Hallo Welt".getBytes(StandardCharsets.UTF_8));  
            in.close();  
            out.close();  
            acceptedConnection.close();  
            serverSocket.close();  
        } catch (IOException e) {  
            throw new RuntimeException(e);  
        }  
    }  
}
```

Als erstes müssen wir mit der `ServerSocket` Klasse ein Socket auf einem PORT öffnen und dann mit `serverSocket.accept()` auf eine Connection warten. Sobald sich ein Client verbunden hat, kann dieser an den Socket nachrichten senden.
Wir können dies mit der Console mit der nc - Anwendung machen:
```bash
nc localhost 8081
Hallo
```

Auf windows ist es etwas schwieriger, man kann dies mit telnet machen:
```bash
telnet localhost 8081
Hallo
```

Oder Powershell falls telnet nicht verfügbar ist:
```powershell
$client = New-Object System.Net.Sockets.TcpClient("localhost",8081)
$stream = $client.GetStream()
$writer = New-Object System.IO.StreamWriter($stream)
$writer.AutoFlush = $true
$writer.WriteLine("Hallo")
$client.Close()
```

---

## HTTP 
Dokumentation zu HTTP Requests: https://datatracker.ietf.org/doc/html/rfc2616 

Nun sprechen wir über das Hyper Text Transfer Protokoll kurz HTTP. 
HTTP Verwendet man heutzutage für den Großteil der Netztwerkkommunikation.
## HTTP Request ohne Body
Als erstes nehmen wir ein HTTP Request ohne Body unter die Lupe:
```http
GET /index HTTP/1.1\r\n
Host: localhost:8080\r\n
Accept: text/html\r\n
Connection: close\r\n

```

Aus welchen Teilen besteht der Request?
- 1 **Request Line**
- 2 **Request Header**
- 3 Leere Zeile
- 4 **Request Body**

Als nächstes nehmen wir uns die Bestandteile jeder der Zeilen vor.

## [Request Line](https://datatracker.ietf.org/doc/html/rfc2616#section-5.1)

```http
POST /index HTTP/1.1\r\n
```

Die Request Line enthält die Request Methode, die Request-URI  und die Protokollversion.  
Die Zeile MUSS durch ein `<CRLF>` also `\r\n` beendet werden.
#### [Methode](https://datatracker.ietf.org/doc/html/rfc2616#section-5.1.1)
Die Methode signalisiert dem Server um welche Art von Request es sich handelt.
Die häufigsten Request Methoden sind: GET, POST, DELETE und PUT .
#### [Request-Uri](https://datatracker.ietf.org/doc/html/rfc2616#section-5.1.2)
Die Uri gibt die Adresse der Resource, die man auf dem Zielserver abrufen will, an.
#### [Protocol Version](https://datatracker.ietf.org/doc/html/rfc2616#section-3.1)
Die Version des Protocols den man für die Kommunikation verwenden möchte.

## [Request Header](https://datatracker.ietf.org/doc/html/rfc2616#section-5.3)

```http
Host: localhost:8080\r\n
Accept: text/html\r\n
Connection: close\r\n
```
Direkt nach der Request Linie kommt eine Reihe von Headern. Zwischen der Request Line und den Request Headern dürfen keine Leerzeilen existieren.
Jeder Header muss durch das `CRLF` Zeichen, also `\r\n` terminiert werden. Es dürfen keine Leerzeilen zwischen den Headern existieren.
Nach dem Letzten Request Header MUSS eine Leerzeile folgen.

Die Header bestehen aus einem Schlüssel Wert paar und modifizieren den Request.

--- 
## [HTTP Request mit Body](https://datatracker.ietf.org/doc/html/rfc2616#section-4.3)

Als nächstes schauen wir uns ein HTTP Request mit Body an:
```http
POST /index HTTP/1.1\r\n
Host: localhost:8080\r\n
Accept: text/html\r\n
Connection: keep-alive\r\n
Content-Type: application/json\r\n
Keep-Alive: timeout=50\r\n
Content-Length: 19\r\n

{
	"Hallo":"Welt"
}
```

Der Request hat den selben Aufbau bis auf ein Paar Header und die Entity, die auch RequestBody genannt wird:

```http
Content-Length: 19\r\n

{
	"Hallo":"Welt"
}
```

Ein HTTP Request mit einem Body muss den Header `Content-Length`enthalten. Dieser gibt die Anzahl der Bytes an aus denen der Body besteht. Dadurch weis der Server wann der Request beendet wird. Denn jetzt sucht der Server nicht mehr nach der Leerzeile am Ende des Requests sondern zählt die Zeichen nach dieser Leerzeile.
Somit kann der Request Body nicht vollständig ausgelesen werden wenn die Content-Length falsch ist.

--- 

## [HTTP Response](https://datatracker.ietf.org/doc/html/rfc2616#section-6)

Nachdem wir gesehen haben, wie ein Request aufgebaut ist, schauen wir uns jetzt an, wie der **Server antwortet**.  
Ein HTTP Response besteht aus drei Teilen:
1. **Status Line**
2. **Response Header**
3. **Body**

Am Ende des Headers muss immer eine **leere Zeile** stehen, danach folgt der Body.

### [1. Status Line](https://datatracker.ietf.org/doc/html/rfc2616#section-6.1)

Die erste Zeile eines HTTP Response nennt man **Status Line**. Sie enthält drei Informationen:
- **Protokollversion** (z. B. HTTP/1.1)
- **Statuscode** (z. B. 200, 404, 500)
- **Reason Phrase** (eine kurze Textbeschreibung des Statuscodes, z. B. OK, Not Found)

```http
HTTP/1.1 200 OK\r\n
```
- `HTTP/1.1`: Version des Protokolls
- `200`  Statuscode, bedeutet „Alles OK“
- `OK`  Kurze Beschreibung

### [2. Response Header](https://datatracker.ietf.org/doc/html/rfc2616#section-6.2)
Nach der Status Line kommen die **Header**, die dem Client zusätzliche Informationen geben.
Jeder Header besteht aus einem **Schlüssel-Wert-Paar**, getrennt durch `:`, und endet mit `\r\n`.
**Beispiele:**
```http
Content-Type: text/html\r\n
Content-Length: 11\r\n
Connection: close\r\n
```

- **Content-Type**: Gibt an, welche Art von Daten der Body enthält (HTML, JSON, Text, …)
- **Content-Length**: Anzahl der Bytes im Body. So weiß der Client, wann die Nachricht endet
- **Connection** : Steuert die Verbindung. `close` bedeutet, dass der Server die Verbindung nach dem Response schließt
### 3. Leerzeile
Nach den Headern muss **eine leere Zeile** (`\r\n`) stehen.  
Sie signalisiert dem Client, dass jetzt der **Body** kommt.
### [4. Body](https://datatracker.ietf.org/doc/html/rfc2616#section-7)
Der Body enthält die eigentlichen Daten, die der Server zurücksendet.  
Beispiel:
```html
Hallo Welt!
```
- Der Body kann HTML, JSON, Text oder andere Formate enthalten.
- Die Länge muss mit `Content-Length` übereinstimmen, sonst kann der Client den Body nicht korrekt erkennen.
### Vollständiger Response

Wenn wir alles zusammenfügen, sieht ein einfacher Response so aus:

```http
HTTP/1.1 200 OK\r\n
Content-Type: text/html\r\n
Content-Length: 11\r\n
Connection: close\r\n
\r\n
Hallo Welt!
```

- Erste Zeile : Status Line
- Nächste Zeilen : Header
- Leere Zeile : trennt Header und Body
- Danach : Body mit dem Text „Hallo Welt!“

--- 
## HTTP Server mit Java

Lasst uns den HTTP Server bauen. Er soll auf ein HTTP Request lauschen und mit einem validen Response antworten.
```java
public class Server{

	public static void main(String[] args){  
	    new Server().start(8081);  
	}  
	public void start(int port){  
	    try {  
	        ServerSocket serverSocket = new ServerSocket(port);  
	        Socket acceptedConnection = serverSocket.accept();  
	  
	        OutputStream out = acceptedConnection.getOutputStream();  
	        BufferedReader in = new BufferedReader(new InputStreamReader(  
	                acceptedConnection.getInputStream()));  
	        String inputLine;  
	        while ((inputLine = in.readLine()) != null) {  
	            System.out.println(inputLine);  
	            if(inputLine.equals("")){  
	                System.out.println("exiting");  
	                break;  
	            }  
	        }  
	        writeResponse(out);  
	        in.close();  
	        out.close();  
	        acceptedConnection.close();  
	        serverSocket.close();  
	    } catch (IOException e) {  
	        throw new RuntimeException(e);  
	    }  
	}  
  
	private void writeResponse(OutputStream out) throws IOException {  
	    out.write(responseStatus.getBytes(StandardCharsets.UTF_8));  
	    out.write(responseHeaderContentType.getBytes(StandardCharsets.UTF_8));  
	    out.write(responseHeaderConnection.getBytes(StandardCharsets.UTF_8));  
	    out.write("\r\n".getBytes(StandardCharsets.UTF_8));  
	    out.write(responseBody.getBytes(StandardCharsets.UTF_8));  
	    out.flush();  
	}  
	  
	String responseStatus = "HTTP/1.1 200 OK\r\n";  
	String responseHeaderContentType = "Content-Type: text/html\r\n" ;  
	String responseHeaderContentLength = "Content-Length: 11\r\n";  
	String responseHeaderConnection = "Connection: close\r\n";  
	String responseBody = "Hallo Welt!";
}
```

Wenn wir jetzt die Anwendung starten und den Browser gegen `localhost:8081` navigieren lassen sollte der Browser `Hallo Welt!` anzeigen.
Gratulation! Ihr habt gelernt wie man mit einem Browser spricht und wie das HTTP funktioniert.
Als nächstes werden wir unseren Server nach und nach erweitern um mehrere Anfragen bearbeiten zu können.
