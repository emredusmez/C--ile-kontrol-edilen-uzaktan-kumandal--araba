#include <SPI.h>
#include <nRF24L01p.h>
#include <Servo.h>

Servo myservo;  // create servo object to control a servo
// a maximum of eight servo objects can be created

int pos = 0;
nRF24L01p alici(7, 8);
/* CSN - > 7, CE -> 8 olarak belirlendi */

void setup() {
  Serial.begin(9600);
  SPI.begin();
  SPI.setBitOrder(MSBFIRST);
  /* SPI başlatıldı */
  alici.channel(90);
  alici.RXaddress("Hasbi");
  alici.init();
  /* Alıcı ayarları yapıldı */

  myservo.attach(9);  // attaches the servo on pin 9 to the servo object

  pinMode(6, OUTPUT);
  pinMode(5, OUTPUT);
  pinMode(3, OUTPUT);
  pinMode(2, OUTPUT);
}

String gelenveri1;

void loop() {
  while (alici.available()) {
    /* Modülden veri geldiği sürece while devam edecek */
    alici.read();
    alici.rxPL(gelenveri1);
    /* Modülden gelen veri okundu */
    if (gelenveri1.length() > 0)
    {
      Serial.println(gelenveri1);
      /* modülden gelen veri ekrana yazdırıldı */

      /* eski veri temizlendi */


      String ilerigeri;
      String hiz;
      String donus;
      if (gelenveri1.length() == 8)
      {
        ilerigeri = gelenveri1.substring(0, 1);
        hiz = gelenveri1.substring(1, 4);
        donus = gelenveri1.substring(4, 7);
        if (gelenveri1.substring(7, 8) == "1")
        {
          digitalWrite(2, HIGH);
        }
        else
          {
            digitalWrite(2, LOW);
          }

        }
      else if (gelenveri1.length() == 9)
      {
        ilerigeri = gelenveri1.substring(2, 3);
        hiz = gelenveri1.substring(3, 6);
        donus = gelenveri1.substring(6, 9);
      }

      if (ilerigeri == "1")
      {
        ileri(hiz.toInt());


      }
      else if (ilerigeri == "0")
      {
        geri(hiz.toInt());
      }
      sagsolyap(donus.toInt());

      gelenveri1 = "";
      ilerigeri = "";
    }
  }
}

void sagsolyap(byte deger)
{
  myservo.write(deger);
  // ORTA NOKTA 95
  //SAĞ NOKTA 110
  //SOL NOKTA 80
}
void ileri(byte hiz)
{
  analogWrite(3, hiz);
  digitalWrite(6, LOW);
  digitalWrite(5, HIGH);

}


void geri(byte hiz)
{
  analogWrite(3, hiz);
  digitalWrite(6, HIGH);
  digitalWrite(5, LOW);

}
