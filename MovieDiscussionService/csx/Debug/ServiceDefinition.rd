<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" name="MovieDiscussionService" generation="1" functional="0" release="0" Id="13791f61-bab6-45be-9b43-826ff4ed2475" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="MovieDiscussionServiceGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="HealthMonitoringService:HttpIn" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/LB:HealthMonitoringService:HttpIn" />
          </inToChannel>
        </inPort>
        <inPort name="HealthStatusService_WebRole:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/LB:HealthStatusService_WebRole:Endpoint1" />
          </inToChannel>
        </inPort>
        <inPort name="MovieDiscussionService_WebRole:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/LB:MovieDiscussionService_WebRole:Endpoint1" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="HealthMonitoringService:DataConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/MapHealthMonitoringService:DataConnectionString" />
          </maps>
        </aCS>
        <aCS name="HealthMonitoringServiceInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/MapHealthMonitoringServiceInstances" />
          </maps>
        </aCS>
        <aCS name="HealthStatusService_WebRole:DataConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/MapHealthStatusService_WebRole:DataConnectionString" />
          </maps>
        </aCS>
        <aCS name="HealthStatusService_WebRoleInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/MapHealthStatusService_WebRoleInstances" />
          </maps>
        </aCS>
        <aCS name="MovieDiscussionService_WebRole:DataConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/MapMovieDiscussionService_WebRole:DataConnectionString" />
          </maps>
        </aCS>
        <aCS name="MovieDiscussionService_WebRoleInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/MapMovieDiscussionService_WebRoleInstances" />
          </maps>
        </aCS>
        <aCS name="NotificationService:DataConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/MapNotificationService:DataConnectionString" />
          </maps>
        </aCS>
        <aCS name="NotificationServiceInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/MapNotificationServiceInstances" />
          </maps>
        </aCS>
        <aCS name="WorkerRoleServiceInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/MapWorkerRoleServiceInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:HealthMonitoringService:HttpIn">
          <toPorts>
            <inPortMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/HealthMonitoringService/HttpIn" />
          </toPorts>
        </lBChannel>
        <lBChannel name="LB:HealthStatusService_WebRole:Endpoint1">
          <toPorts>
            <inPortMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/HealthStatusService_WebRole/Endpoint1" />
          </toPorts>
        </lBChannel>
        <lBChannel name="LB:MovieDiscussionService_WebRole:Endpoint1">
          <toPorts>
            <inPortMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/MovieDiscussionService_WebRole/Endpoint1" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapHealthMonitoringService:DataConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/HealthMonitoringService/DataConnectionString" />
          </setting>
        </map>
        <map name="MapHealthMonitoringServiceInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/HealthMonitoringServiceInstances" />
          </setting>
        </map>
        <map name="MapHealthStatusService_WebRole:DataConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/HealthStatusService_WebRole/DataConnectionString" />
          </setting>
        </map>
        <map name="MapHealthStatusService_WebRoleInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/HealthStatusService_WebRoleInstances" />
          </setting>
        </map>
        <map name="MapMovieDiscussionService_WebRole:DataConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/MovieDiscussionService_WebRole/DataConnectionString" />
          </setting>
        </map>
        <map name="MapMovieDiscussionService_WebRoleInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/MovieDiscussionService_WebRoleInstances" />
          </setting>
        </map>
        <map name="MapNotificationService:DataConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/NotificationService/DataConnectionString" />
          </setting>
        </map>
        <map name="MapNotificationServiceInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/NotificationServiceInstances" />
          </setting>
        </map>
        <map name="MapWorkerRoleServiceInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/WorkerRoleServiceInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="HealthMonitoringService" generation="1" functional="0" release="0" software="E:\Documents\Predavanja\IV_Godina\CLOUD\projekat\MovieDiscussionService\MovieDiscussionService\csx\Debug\roles\HealthMonitoringService" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaWorkerHost.exe " memIndex="-1" hostingEnvironment="consoleroleadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="HttpIn" protocol="http" portRanges="50002" />
            </componentports>
            <settings>
              <aCS name="DataConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;HealthMonitoringService&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;HealthMonitoringService&quot;&gt;&lt;e name=&quot;HttpIn&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;HealthStatusService_WebRole&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;MovieDiscussionService_WebRole&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;NotificationService&quot; /&gt;&lt;r name=&quot;WorkerRoleService&quot; /&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/HealthMonitoringServiceInstances" />
            <sCSPolicyUpdateDomainMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/HealthMonitoringServiceUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/HealthMonitoringServiceFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
        <groupHascomponents>
          <role name="HealthStatusService_WebRole" generation="1" functional="0" release="0" software="E:\Documents\Predavanja\IV_Godina\CLOUD\projekat\MovieDiscussionService\MovieDiscussionService\csx\Debug\roles\HealthStatusService_WebRole" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="-1" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="8080" />
            </componentports>
            <settings>
              <aCS name="DataConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;HealthStatusService_WebRole&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;HealthMonitoringService&quot;&gt;&lt;e name=&quot;HttpIn&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;HealthStatusService_WebRole&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;MovieDiscussionService_WebRole&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;NotificationService&quot; /&gt;&lt;r name=&quot;WorkerRoleService&quot; /&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/HealthStatusService_WebRoleInstances" />
            <sCSPolicyUpdateDomainMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/HealthStatusService_WebRoleUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/HealthStatusService_WebRoleFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
        <groupHascomponents>
          <role name="MovieDiscussionService_WebRole" generation="1" functional="0" release="0" software="E:\Documents\Predavanja\IV_Godina\CLOUD\projekat\MovieDiscussionService\MovieDiscussionService\csx\Debug\roles\MovieDiscussionService_WebRole" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="-1" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="80" />
            </componentports>
            <settings>
              <aCS name="DataConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;MovieDiscussionService_WebRole&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;HealthMonitoringService&quot;&gt;&lt;e name=&quot;HttpIn&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;HealthStatusService_WebRole&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;MovieDiscussionService_WebRole&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;NotificationService&quot; /&gt;&lt;r name=&quot;WorkerRoleService&quot; /&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/MovieDiscussionService_WebRoleInstances" />
            <sCSPolicyUpdateDomainMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/MovieDiscussionService_WebRoleUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/MovieDiscussionService_WebRoleFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
        <groupHascomponents>
          <role name="NotificationService" generation="1" functional="0" release="0" software="E:\Documents\Predavanja\IV_Godina\CLOUD\projekat\MovieDiscussionService\MovieDiscussionService\csx\Debug\roles\NotificationService" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaWorkerHost.exe " memIndex="-1" hostingEnvironment="consoleroleadmin" hostingEnvironmentVersion="2">
            <settings>
              <aCS name="DataConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;NotificationService&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;HealthMonitoringService&quot;&gt;&lt;e name=&quot;HttpIn&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;HealthStatusService_WebRole&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;MovieDiscussionService_WebRole&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;NotificationService&quot; /&gt;&lt;r name=&quot;WorkerRoleService&quot; /&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/NotificationServiceInstances" />
            <sCSPolicyUpdateDomainMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/NotificationServiceUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/NotificationServiceFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
        <groupHascomponents>
          <role name="WorkerRoleService" generation="1" functional="0" release="0" software="E:\Documents\Predavanja\IV_Godina\CLOUD\projekat\MovieDiscussionService\MovieDiscussionService\csx\Debug\roles\WorkerRoleService" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaWorkerHost.exe " memIndex="-1" hostingEnvironment="consoleroleadmin" hostingEnvironmentVersion="2">
            <settings>
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;WorkerRoleService&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;HealthMonitoringService&quot;&gt;&lt;e name=&quot;HttpIn&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;HealthStatusService_WebRole&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;MovieDiscussionService_WebRole&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;NotificationService&quot; /&gt;&lt;r name=&quot;WorkerRoleService&quot; /&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/WorkerRoleServiceInstances" />
            <sCSPolicyUpdateDomainMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/WorkerRoleServiceUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/WorkerRoleServiceFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="MovieDiscussionService_WebRoleUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyUpdateDomain name="HealthStatusService_WebRoleUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyUpdateDomain name="NotificationServiceUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyUpdateDomain name="WorkerRoleServiceUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyUpdateDomain name="HealthMonitoringServiceUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="HealthMonitoringServiceFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyFaultDomain name="HealthStatusService_WebRoleFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyFaultDomain name="MovieDiscussionService_WebRoleFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyFaultDomain name="NotificationServiceFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyFaultDomain name="WorkerRoleServiceFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="HealthMonitoringServiceInstances" defaultPolicy="[1,1,1]" />
        <sCSPolicyID name="HealthStatusService_WebRoleInstances" defaultPolicy="[1,1,1]" />
        <sCSPolicyID name="MovieDiscussionService_WebRoleInstances" defaultPolicy="[1,1,1]" />
        <sCSPolicyID name="NotificationServiceInstances" defaultPolicy="[1,1,1]" />
        <sCSPolicyID name="WorkerRoleServiceInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="ecfbede1-d7ad-41b7-a000-c370f542366e" ref="Microsoft.RedDog.Contract\ServiceContract\MovieDiscussionServiceContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="302113ab-6796-4c47-939d-74175c96d2d8" ref="Microsoft.RedDog.Contract\Interface\HealthMonitoringService:HttpIn@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/HealthMonitoringService:HttpIn" />
          </inPort>
        </interfaceReference>
        <interfaceReference Id="6e2c1f61-2c7c-4703-83dc-3fb2b173e2ae" ref="Microsoft.RedDog.Contract\Interface\HealthStatusService_WebRole:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/HealthStatusService_WebRole:Endpoint1" />
          </inPort>
        </interfaceReference>
        <interfaceReference Id="f1232064-76cd-4670-9491-fff765b6b4a8" ref="Microsoft.RedDog.Contract\Interface\MovieDiscussionService_WebRole:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/MovieDiscussionService_WebRole:Endpoint1" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>