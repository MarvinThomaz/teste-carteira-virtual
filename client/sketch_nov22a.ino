#include <SPI.h>
#include <MFRC522.h>
#include <Bridge.h>
#include <HttpClient.h>
/*
 * Variáveis globais com as portas do dispositivo RFID
 */
#define RST_PIN 9
#define SS_PIN 10

/*
 * Variável de controle do dispositivo RFID
 */
MFRC522 mfrc522(SS_PIN, RST_PIN);

/*
 * Inicialização das portas.
 */
void setup() {
  configure_rfid();
  configure_valve();
  configure_http();
}

/*
 * Criação do looping de execução.
 */
void loop() {
  String id = read_rfid_card();
  
  if(id) {
    String url = "https://chopp-cart.herokuapp.com/api/carts/" + id;

    String data = request(url);
    
    open_valve();
  }
}

/*
 * Configuração do rfid
 */
void configure_rfid() {
  Serial.begin(9600);
  
  while (!Serial) {
    SPI.begin();
  }
  
  mfrc522.PCD_Init();
}

/*
 * Configuração da valvula
 */
void configure_valve() {
  pinMode(7, OUTPUT);
}

/*
 * Leitura da pulseira
 */
String read_rfid_card() {
  if (!mfrc522.PICC_IsNewCardPresent()) {
    return;
  }

  if (!mfrc522.PICC_ReadCardSerial()) {
    return;
  }

  String uid = convert_uid(mfrc522);

  return uid;
}

/*
 * Converte o id da pulseira para long
 */
String convert_uid(MFRC522 mfrc522)
{
  String UID_unsigned;
  
  UID_unsigned =  mfrc522.uid.uidByte[0];
  UID_unsigned += mfrc522.uid.uidByte[1];
  UID_unsigned += mfrc522.uid.uidByte[2];
  UID_unsigned += mfrc522.uid.uidByte[3];
  
  return UID_unsigned;
}

/*
 * Abre a válvula
 */
void open_valve() {
  digitalWrite(7, HIGH);
}

/*
 * Fecha a valvula
 */
void close_valve() {
  digitalWrite(7, LOW);
}

/*
 * Configura cliente http
 */
void configure_http() {
    pinMode(13, OUTPUT);
    
    digitalWrite(13, LOW);
    
    Bridge.begin();
    
    Serial.begin(9600);
    
    while(!Serial);
}

/*
 * Realiza requisição de busca do cliente
 */
String request(String url) {
  HttpClient http;
  
  int index = 0, result = http.get(url);
  
  String data;
  
  while (http.available()) {
    data += http.read();
    
    index++;
  }
  
  Serial.flush();

  delay(5000);

  return data;
}