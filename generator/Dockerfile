FROM python:3.6

RUN mkdir /application
WORKDIR "/application"

RUN pip install --upgrade pip

RUN apt-get update \
    && apt-get clean; rm -rf /var/lib/apt/lists/* /tmp/* /var/tmp/* /usr/share/doc/*

ADD requirements.txt /application/
ADD generator/main.py /application/

RUN pip install -r /application/requirements.txt

CMD [ "python", "main.py" ]