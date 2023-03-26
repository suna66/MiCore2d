#version 330

out vec4 outputColor;

in vec2 texCoord;

uniform sampler2D texture0;
uniform float texAlpha;
uniform float scale;
uniform float direct;

void main()
{
  float length = 0.0;
  if (direct == 0.0)
  {
    length = texCoord.x * scale;
  }
  else
  {
    length = 1.0 - (texCoord.x * scale);
  }
  float alpha = texAlpha - length;

  outputColor = mix(texture(texture0, texCoord), vec4(0.0, 0.0, 0.0, 0.0), length);
}
