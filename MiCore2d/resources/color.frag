#version 330

out vec4 outputColor;

in vec2 texCoord;

uniform sampler2D texture0;
uniform float texAlpha;

void main()
{
  vec4 color = texture(texture0, texCoord).rgba;
  if (color.a != 0.0) {
    color.r = 0.0;
    color.g = 0.0;
    color.b = 1.0;
  }
  outputColor = mix(color, vec4(0.0, 0.0, 0.0, 0.0), texAlpha);
}
