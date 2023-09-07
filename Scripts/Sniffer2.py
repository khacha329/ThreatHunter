import subprocess
import json
import paho.mqtt.client as mqtt
from IDStorage import IDStorage

# MQTT Broker settings
mqtt_broker = 'test.mosquitto.org'
mqtt_port = 1883
mqtt_topic_subscribe = 'control/command'
mqtt_topic = 'wifi/signal'

id_storage = IDStorage()
 
# MQTT callback functions
def on_connect(client, userdata, flags, rc):
    if rc == 0:
        client.connected_flag = True  # set flag
        print("Connected to MQTT broker with result code " + str(rc))
    else:
        print("Bad connection Returned code=", rc)
    

def on_publish(client, userdata, mid):
    print("Data published to MQTT broker")
    
def on_message(client, userdata, msg):
    if msg.topic == mqtt_topic_subscribe:
        command = msg.payload.decode("utf-8")
        if command in id_storage.id_dict:
           client.publish(mqtt_topic, id_storage.get_strength(command))
	

# Initialize MQTT client
client = mqtt.Client()
client.on_connect = on_connect
client.on_publish = on_publish
client.on_message = on_message



# Connect to MQTT broker
try:
        client.connect(mqtt_broker, mqtt_port)  # connect to broker
        client.loop_start()
        print("connected to mqtt")
        #client.loop(timeout=1.0, max_packets=1)
        client.subscribe(mqtt_topic_subscribe)
except KeyboardInterrupt:
        pass

# Run TShark command and process output in real-time
tshark_cmd = [
    'tshark',
    '-i', 'wlan0',
    '-Y', 'wlan.fc.type_subtype == 0x08 && wlan_radio.signal_dbm != -127',
    '-T', 'ek'
]

tshark_process = subprocess.Popen(tshark_cmd, stdout=subprocess.PIPE, universal_newlines=True)
for line in tshark_process.stdout:
    packet_data = json.loads(line)
    if(len(packet_data) <=1):  # to filter the json with 1 element sent before every sniffed packet
        continue
    else:  	
        signal_strength = packet_data["layers"]['wlan_radio']['wlan_radio_wlan_radio_signal_dbm']
        bssid = packet_data['layers']['wlan']['wlan_wlan_bssid']
        if bssid not in id_storage.id_dict:
            client.publish(mqtt_topic, bssid)
        id_storage.add_id(bssid, signal_strength)                  
        payload = f"Signal: {signal_strength} dBm, BSSID: {bssid}"
        #print(payload)  
        

# Clean up
tshark_process.terminate()
client.loop_stop()
client.disconnect()
