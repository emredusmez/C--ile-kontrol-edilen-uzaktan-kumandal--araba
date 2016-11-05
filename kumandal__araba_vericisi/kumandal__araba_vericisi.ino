#include <SPI.h>
#include <nRF24L01p.h>
#include <String.h>

nRF24L01p verici(7,8);
/* CSN - > 7, CE -> 8 olarak belirlendi */

float sicaklik;
static char veri[10];

void setup() {
  Serial.begin(9600);
  SPI.begin();
  SPI.setBitOrder(MSBFIRST);
  /* SPI başlatıldı */
  verici.channel(90);
  verici.TXaddress("Hasbi");
  verici.init();
  /* Verici ayarları yapıldı */
}
String gelenveri1;
void loop() {



  
   char inChar;
  while (Serial.available() > 0) // Don't read unless
    // there you know there is data
  {

    inChar = Serial.read(); // Read a character
    if (inChar != '.')
    {
      gelenveri1 = gelenveri1 + inChar;
      
      
    }
    else
    {
  
  verici.txPL(gelenveri1);
  boolean gonderimDurumu = verici.send(FAST);
  /* Sıcaklık bilgisi nRF24L01'e aktarıldı */
  /* Eğer gönderim başarısız olursa gonderimDurumu'nun değeri false olacaktır */
 
  gelenveri1="";
  
    }
}
}
