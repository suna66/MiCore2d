#version 330

out vec4 outputColor;

in vec2 texCoord;

uniform sampler2D texture0;
uniform float texAlpha;
uniform float scale;
uniform float direct;

void main()
{
  float len = 1.0 - scale;
  if (direct >= 0.0)
  {
    if (texCoord.x < len)
    {
      outputColor = mix(texture(texture0, texCoord), vec4(0.0, 0.0, 0.0, 0.0), texAlpha);
    }
    else
    {
      float alpha = 1.0;
      float dis = texCoord.x - len;
      if (dis < 0.1)
      {
        alpha = dis * 10.0f;
      }

      outputColor = mix(texture(texture0, texCoord), vec4(0.0, 0.0, 0.0, 0.0), alpha);
    }
  }
  else
  {
    if (texCoord.x < len)
    {
      float alpha = 1.0;
      float dis = len - texCoord.x;
      if (dis < 0.1)
      {
        alpha = dis * 10.0f;
      }
      outputColor = mix(texture(texture0, texCoord), vec4(0.0, 0.0, 0.0, 0.0), alpha);
    }
    else
    {
      outputColor = mix(texture(texture0, texCoord), vec4(0.0, 0.0, 0.0, 0.0), texAlpha);
    }
  }  
}
