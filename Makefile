.PHONY: all build-images

all: build-images

ARCH ?= x64
build-images: 
	dotnet publish AuthService/src/Web/Web.csproj --os linux --arch $(ARCH) \
		/t:PublishContainer -c Release --self-contained true && \
	dotnet publish NotificationService/src/Web/Web.csproj --os linux --arch $(ARCH) \
		/t:PublishContainer -c Release --self-contained true && \
	dotnet publish ApiGateway/ApiGateway.csproj --os linux --arch $(ARCH) \
		/t:PublishContainer -c Release --self-contained true


