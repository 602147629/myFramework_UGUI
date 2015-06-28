require "common/functions"

local trans;
local baseLua;
local gameObject;

local this;
LoginPanel = {};

--启动事件--
function LoginPanel.Start()
	this = LoginPanel;
	gameObject = find("LoginPanel");
	trans = gameObject.transform;
	baseLua = gameObject:GetComponent('BaseLua');

     -- 赋值回调
    baseLua.callBack = function()
        warn("OnCallBack---->>>");
    end

	baseLua:AddClick('LoginBtn', this.OnClick);
	--ioo.resourceManager:LoadUIAsset('prompt', 'PromptItem', this.InitPanel);
	warn("Start lua--->>"..gameObject.name);
end

--初始化面板--
function LoginPanel.InitPanel()
	
end

--单击事件--
function LoginPanel.OnClick(obj)
  
   --[[ local buffer = ByteBuffer.New();
    buffer:WriteShort(Login);
    buffer:WriteString("ffff我的ffffQ靈uuu");
    buffer:WriteInt(200);
    ioo.networkManager:SendMessage(buffer);]]

    local loginBtn = trans:FindChild("LoginBtn").gameObject;

    --baseLua:TweenDelayedCall(1.0, loginBtn);

    --LeanTween.move(loginBtn:GetComponent("RectTransform"), Vector3.New(0,0,0), 1.0):setDelay(1);

    createPanel("Prompt");

	warn("OnClick---->>>"..obj.name);

    destroy(gameObject);
end

function LoginPanel.OnTweenFinished(obj)

    warn("OnTweenFinished---->>>"..obj.name);
end