#version 330

out vec4 outputColor;

in vec2 texCoord;

uniform sampler2D texture0;
uniform float texAlpha;
uniform float speed;
uniform float length;
uniform float width;
uniform float time;

void main()
{
  float offsetX = sin(texCoord.x * length + time * speed) * width;
  float offsetY = cos(texCoord.y * length + time * speed) * width;
  outputColor = mix(texture(texture0, vec2(texCoord.x + offsetX, texCoord.y + offsetY)), vec4(0.0, 0.0, 0.0, 0.0), texAlpha);
}
