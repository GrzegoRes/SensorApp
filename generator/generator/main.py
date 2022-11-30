import json
import threading
import datetime
import pika
import time
import random
from enum import Enum
from typing import NamedTuple


# class Air(NamedTuple):
#     nitrogen: float
#     oxygen: float
#     argon: float
#     carbon_dioxide: float


class Type(Enum):
    TEMPERATURE = 1
    LIGHT = 2
    HUMIDITY = 3
    CARBON_DIOXIDE = 4
    # GAS_CONCENTRATION = 4


class MyThread(threading.Thread):
    def __init__(self, threadID, name, counter, sensor, delay, channel):
        super().__init__()
        self.threadID = threadID
        self.name = name
        self.counter = counter
        self.sensor = sensor
        self.delay = delay
        self.channel = channel

    def send_msg(self):
        # print(f'{self.threadID}\n')
        # print(self.sensor.to_string())
        self.channel.basic_publish(exchange='',
                                   routing_key='sensor.04',
                                   body=self.sensor.to_string())

    def operations(self):
        counter = self.counter
        while counter:
            time.sleep(self.delay)
            self.sensor.generate_value()
            self.send_msg()
            # print("%s: %s" % (self.name, time.ctime(time.time())))
            counter -= 1

    def run(self):
        # print("Starting " + self.name)
        self.operations()
        # print_time(self.name, self.counter, self.sensors[0].get_per_min())
        # print("Exiting " + self.name)


# def print_time(threadName, counter, delay):
#     while counter:
#         time.sleep(delay)
#
#         print("%s: %s" % (threadName, time.ctime(time.time())))
#         counter -= 1


class Sensor:
    def __init__(self, id, name):
        self._id = id
        self._name = name
        self._type = None
        self._min_value = None
        self._max_value = None
        self._per_min = None
        self._value = None
        self._unit = None
        self._date = f'{datetime.datetime.now().strftime("%Y-%m-%d %H:%M:%S")}'

    def get_type(self):
        return self._type

    def get_per_min(self):
        return self._per_min

    # def set_min_value(self, value):
    #     self._min_value = value
    #
    # def set_max_value(self, value):
    #     self._max_value = value

    def generate_value(self):
        self._date = f'{datetime.datetime.now().strftime("%Y-%m-%d %H:%M:%S")}'
        self._value = random.randrange(self._min_value, self._max_value + 1)

    def to_string(self):
        return f'{self._name}|{self._type}|{self._value}|{self._unit}|{self._date}'


class TemperatureSensor(Sensor):
    def __init__(self, id, name):
        super().__init__(id, name)
        self._type = Type.TEMPERATURE.name
        self._min_value = 0
        self._max_value = 60
        self._per_min = 20
        self._unit = 'C'


class LightSensor(Sensor):
    def __init__(self, id, name):
        super().__init__(id, name)
        self._type = Type.LIGHT.name
        self._min_value = 0
        self._max_value = 300
        self._per_min = 60
        self._unit = 'lm'


class HumiditySensor(Sensor):
    def __init__(self, id, name):
        super().__init__(id, name)
        self._type = Type.HUMIDITY.name
        self._min_value = 0
        self._max_value = 100
        self._per_min = 10
        self._unit = '%'


class CarbonDioxideSensor(Sensor):
    def __init__(self, id, name):
        super().__init__(id, name)
        self._type = Type.CARBON_DIOXIDE.name
        self._min_value = 0
        self._max_value = 10
        self._per_min = 5
        self._unit = '%'


def generate_sensor():
    id = 1
    while True:
        type_number = random.randrange(1, len(Type) + 1)
        name = f'{Type(type_number).name}{id}'

        if Type(type_number) == Type.TEMPERATURE:
            yield TemperatureSensor(id, name)
        elif Type(type_number) == Type.LIGHT:
            yield LightSensor(id, name)
        elif Type(type_number) == Type.HUMIDITY:
            yield HumiditySensor(id, name)
        elif Type(type_number) == Type.CARBON_DIOXIDE:
            yield CarbonDioxideSensor(id, name)
            # nitrogen = 78.05 + float(random.randrange(6)) / 100.00  # random <78.05, 78.10>
            # oxygen = 20.00 + float(random.randrange(90, 96)) / 100.00  # random <20.90, 20.95>
            # argon = 0.90 + float(random.randrange(5)) / 100.00  # random <00.90, 00.95)
            # carbon_dioxide = 100.00 - nitrogen - oxygen - argon  # random <00.01, 00.15>
            # yield GasConcentrationSensor(id, name, Air(nitrogen, oxygen, argon, carbon_dioxide))

        id += 1


def generate_sensor_list():
    sensor_gen = generate_sensor()
    list = []
    for _ in range(30):
        sensor = next(sensor_gen)
        list.append(sensor)
    return list


if __name__ == '__main__':
    sensor_list = generate_sensor_list()
    temp_sensors, light_sensors, hum_sensors, carbon_sensors = ([] for i in range(4))
    threads = []

    # connection = pika.BlockingConnection(pika.ConnectionParameters('localhost', heartbeat=5))
    # channel = connection.channel()
    # channel.queue_declare(queue='sensor.02')

    # channel.basic_publish(exchange='',
    #                     routing_key='sensor.02',
    #                     body='ss')
    for i, sensor in enumerate(sensor_list):
        # print(f'{"Thread-"}{sensor.get_type()}{i}')
        # print(sensor.to_string())
        credentials2 = pika.credentials.PlainCredentials(
            username="guest",
            password="guest"
        )

        connection = pika.BlockingConnection(pika.ConnectionParameters('rabbitmq', heartbeat=30))
        channel = connection.channel()
        channel.queue_declare(queue='sensor.04')
        threads.append(MyThread(i, f'{"Thread-"}{sensor.get_type()}{i}', 999, sensor, 60 / sensor.get_per_min(),
                                channel))

    for thread in threads:
        thread.start()

    # connection.close()
